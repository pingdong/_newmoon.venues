namespace PingDong.Newmoon.Venues.Settings
{
    public class AppSettings
    {
        public bool SupportIdempotencyCheck { get; set; } = true;

        public AzureBlobStorageSettings BlobStorage { get; set; }
        public AzureTableStorageSettings TableStorage { get; set; }
        public AzureSqlSettings AzureSql { get; set; }
        public DevelopmentSettings Development { get; set; }
    }
}
