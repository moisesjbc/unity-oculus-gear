//
//  IStringBuilder.hpp
//  G3MiOSSDK
//
//  Created by Jos� Miguel S N on 22/08/12.
//  Copyright (c) 2012 __MyCompanyName__. All rights reserved.
//



public abstract class IStringBuilder
{

  private static IStringBuilder _instance;



//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: virtual IStringBuilder* getNewInstance() const = 0;
  protected abstract IStringBuilder getNewInstance();

//C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
//  static void setInstance(IStringBuilder isb);

//C++ TO C# CONVERTER TODO TASK: The implementation of the following method could not be found:
//  static IStringBuilder newStringBuilder();

  public abstract IStringBuilder addDouble(double d);
  public abstract IStringBuilder addFloat(float f);

  public abstract IStringBuilder addInt(int i);
  public abstract IStringBuilder addLong(long l);

  public abstract IStringBuilder addString(string s);
  public abstract IStringBuilder addBool(bool b);

  public abstract IStringBuilder clear();

//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: virtual const string getString() const = 0;
  public abstract string getString();

  // a virtual destructor is needed for conversion to Java
  public virtual void Dispose()
  {
  }

//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: virtual bool contentEqualsTo(const string& that) const = 0;
  public abstract bool contentEqualsTo(string that);

}
