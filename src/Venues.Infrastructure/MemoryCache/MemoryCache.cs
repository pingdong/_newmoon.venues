using PingDong.Newmoon.Venues.Models;
using System;
using System.Threading;

namespace PingDong.Newmoon.Venues.Infrastructure
{
    internal class MemoryCache : ICache
    {
        // TODO: Async pre-fetch cache

        public Venue Get(Guid id)
        {
            throw new NotImplementedException();
        }

        public void Save(Venue venue)
        {
            throw new NotImplementedException();
        }
        
        // https://xie.infoq.cn/article/a035f12e5590385ac578778b0
        // https://www.jianshu.com/p/090c8767ffea
        // https://docs.microsoft.com/en-us/dotnet/api/system.runtime.caching.memorycache?view=dotnet-plat-ext-3.1

        private Mutex reenLock = new Mutex();

        public String getCacheData()
        {
            String result = "";
            //Read redis
            result = getDataFromRedis();
            if (result.isEmpty())
            {
                if (reenLock.WaitOne())//.tryLock())
                {
                    try
                    {
                        //Read database
                        result = getDataFromDB();
                        //Write redis
                        setDataToCache(result);
                    }
                    catch (Exception e)
                    {
                        //...
                    }
                    finally
                    {
                        reenLock.ReleaseMutex();//.unlock(); // release lock
                    }
                }
                else
                {
                    //Note: this can be combined with the 
                    // following double caching mechanism:

                    //If you can't grab the lock, 
                    // query the secondary cache

                    //Read redis
                    result = getDataFromRedis();
                    if (result.isEmpty())
                    {
                        try
                        {
                            Thread.sleep(100);
                        }
                        catch (InterruptedException e)
                        {
                            //...
                        }
                        return getCacheData();
                    }
                }
            }
            return result;
        }
    }
}
