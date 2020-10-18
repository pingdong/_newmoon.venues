using FluentValidation;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using PingDong.Validations;
using System;
using System.Threading.Tasks;
using Microsoft.ApplicationInsights;
using Microsoft.ApplicationInsights.DataContracts;
using Microsoft.ApplicationInsights.Extensibility;

namespace PingDong.Azure.Function
{
    public abstract class TriggerBase
    {
        #region ctor

        protected TriggerBase(
            TelemetryConfiguration telemetryConfiguration
            , ILogger logger
            , IValidatorFactory validatorFactory)
        {
            Logger = logger.EnsureNotNull(nameof(logger));
            TelemetryClient = new TelemetryClient(telemetryConfiguration);

            _validatorFactory = validatorFactory;
        }

        #endregion

        #region Execution

        protected async Task ProcessAsync(
            ExecutionContext context
            , Func<Task> func)
        {
            // Pre-Process
            var functionName = context.FunctionName;
            var start = DateTime.UtcNow;

            PreProcess(functionName, start);

            try
            {
                // Process
                await func().ConfigureAwait(false);
            }
            finally
            {
                // Post-Process
                PostProcess(functionName, start, DateTime.UtcNow);
            }
        }

        protected async Task<T> ProcessAsync<T>(
            ExecutionContext context
            , Func<Task<T>> func)
        {
            // Pre-Process
            var functionName = context.FunctionName;
            var start = DateTime.UtcNow;

            PreProcess(functionName, start);

            try
            {
                // Process
                return await func().ConfigureAwait(false);
            }
            finally
            {
                // Post-Process
                PostProcess(functionName, start, DateTime.UtcNow);
            }
        }

        #endregion

        #region Properties

        protected ILogger Logger { get; }

        protected TelemetryClient TelemetryClient { get; }

        #endregion

        #region Private Methods
        
        private void PreProcess(string functionName, DateTime start)
        {
            // Track an Event
            var evt = new EventTelemetry($"Function - '{functionName}' started on {start}");
            TelemetryClient.TrackEvent(evt);

            PreProcess();
        }

        private void PostProcess(string functionName, DateTime start, DateTime end)
        {
            PostProcess();

            var elapse = end - start;

            // Track an Event
            var evt = new EventTelemetry($"Function - '{functionName}' ended on {end}");
            TelemetryClient.TrackEvent(evt);

            // Track a Metric
            var metric = new MetricTelemetry($"Function - '{functionName}' spent", elapse.TotalMilliseconds)
            {
                MetricNamespace = $"{functionName}.ProcessedTime"
            };
            TelemetryClient.TrackMetric(metric);

            Logger.LogMetric($"Function - '{functionName}'", elapse.TotalMilliseconds);
        }

        #endregion

        #region Protected Methods

        protected virtual void PreProcess()
        {
        }

        protected virtual void PostProcess()
        {
        }

        #endregion

        #region Validation
        
        private readonly IValidatorFactory _validatorFactory;

        protected async Task ValidateAsync<T>(T entity)
        {
            if (_validatorFactory == null)
                throw new NullReferenceException("IValidatorFactory has initialed properly");

            var validator = _validatorFactory.GetValidator<T>();
            if (validator == null)
                return;
            
            await entity.EnsureValidatedAsync(validator).ConfigureAwait(false);
        }

        #endregion
    }
}
