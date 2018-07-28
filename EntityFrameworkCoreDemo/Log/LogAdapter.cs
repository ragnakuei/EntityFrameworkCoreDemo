using NLog;

namespace EntityFrameworkCoreDemo.Log
{
    public class LogAdapter
    {
        private Logger _logger;

        public void Initial<T>()
        {
            _logger = LogManager.GetLogger(typeof(T).FullName);
        }

        public void Initial(string typeName)
        {
            _logger = LogManager.GetLogger(typeName);
        }

        public void Info(string s)
        {
            _logger.Info(s);
        }

        public void Error(string s)
        {
            _logger.Error(s);
        }

        public void Debug(string s)
        {
            _logger.Debug(s);
        }
    }
}