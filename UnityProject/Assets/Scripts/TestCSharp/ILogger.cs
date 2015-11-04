//
//  ILogger.hpp
//  G3MiOSSDK
//
//  Created by Agustin Trujillo Pino on 31/05/12.
//  Copyright (c) 2012 __MyCompanyName__. All rights reserved.
//





public enum LogLevel
{
  SilenceLevel,
  InfoLevel,
  WarningLevel,
  ErrorLevel
}


public abstract class ILogger
{
  protected readonly LogLevel _level;

  protected ILogger(LogLevel level)
  {
		this._level = level;
  }

  protected static ILogger _instance;

  public static void setInstance(ILogger logger)
  {
	if (_instance != null)
	{
	  ILogger.instance().logWarning("ILooger instance already set!");
	  if (_instance != null)
		  _instance.Dispose();
	}
	_instance = logger;
  }

  public static ILogger instance()
  {
	return _instance;
  }


//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: virtual void logInfo(const string x, ...) const = 0;
  public abstract void logInfo(string x, params object[] LegacyParamArray);
//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: virtual void logWarning(const string x, ...) const = 0;
  public abstract void logWarning(string x, params object[] LegacyParamArray);
//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: virtual void logError(const string x, ...) const = 0;
  public abstract void logError(string x, params object[] LegacyParamArray);

  public virtual void Dispose()
  {
  }

}

