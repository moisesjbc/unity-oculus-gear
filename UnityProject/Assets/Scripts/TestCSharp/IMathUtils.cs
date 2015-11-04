using System;

//
//  IMathUtils.hpp
//  G3MiOSSDK
//
//  Created by Jos� Miguel S N on 24/08/12.
//  Copyright (c) 2012 __MyCompanyName__. All rights reserved.
//


//C++ TO C# CONVERTER NOTE: The following #define macro was replaced in-line:
//ORIGINAL LINE: #define ISNAN(x) (x != x)

public abstract class IMathUtils
{
  private static IMathUtils _instance;

  public static void setInstance(IMathUtils math)
  {
	if (_instance != null)
	{
	  ILogger.instance().logWarning("IMathUtils instance already set!");
	  if (_instance != null)
		  _instance.Dispose();
	}
	_instance = math;
  }

  public static IMathUtils instance()
  {
	return _instance;
  }

  public virtual void Dispose()
  {
  }

//  virtual double NanD() const = 0;
//  virtual float  NanF() const = 0;

//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: virtual double sin(double v) const = 0;
  public abstract double sin(double v);
//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: virtual float sin(float v) const = 0;
  public abstract float sin(float v);

//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: virtual double sinh(double v) const = 0;
  public abstract double sinh(double v);
//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: virtual float sinh(float v) const = 0;
  public abstract float sinh(float v);

//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: virtual double asin(double v) const = 0;
  public abstract double asin(double v);
//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: virtual float asin(float v) const = 0;
  public abstract float asin(float v);

//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: virtual double cos(double v) const = 0;
  public abstract double cos(double v);
//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: virtual float cos(float v) const = 0;
  public abstract float cos(float v);

//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: virtual double acos(double v) const = 0;
  public abstract double acos(double v);
//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: virtual float acos(float v) const = 0;
  public abstract float acos(float v);

//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: virtual double tan(double v) const = 0;
  public abstract double tan(double v);
//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: virtual float tan(float v) const = 0;
  public abstract float tan(float v);

//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: virtual double atan(double v) const = 0;
  public abstract double atan(double v);
//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: virtual float atan(float v) const = 0;
  public abstract float atan(float v);

//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: virtual double atan2(double u, double v) const = 0;
  public abstract double atan2(double u, double v);
//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: virtual float atan2(float u, float v) const = 0;
  public abstract float atan2(float u, float v);

//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: virtual long round(double v) const = 0;
  public abstract long round(double v);
//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: virtual int round(float v) const = 0;
  public abstract int round(float v);

//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: virtual int abs(int v) const = 0;
  public abstract int abs(int v);
//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: virtual double abs(double v) const = 0;
  public abstract double abs(double v);
//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: virtual float abs(float v) const = 0;
  public abstract float abs(float v);

//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: virtual double sqrt(double v) const = 0;
  public abstract double sqrt(double v);
//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: virtual float sqrt(float v) const = 0;
  public abstract float sqrt(float v);

//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: virtual double pow(double v, double u) const = 0;
  public abstract double pow(double v, double u);
//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: virtual float pow(float v, float u) const = 0;
  public abstract float pow(float v, float u);

//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: virtual double exp(double v) const = 0;
  public abstract double exp(double v);
//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: virtual float exp(float v) const = 0;
  public abstract float exp(float v);

//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: virtual double log10(double v) const = 0;
  public abstract double log10(double v);
//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: virtual float log10(float v) const = 0;
  public abstract float log10(float v);

//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: virtual double log(double v) const = 0;
  public abstract double log(double v);
//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: virtual float log(float v) const = 0;
  public abstract float log(float v);

//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: virtual short maxInt16() const = 0;
  public abstract short maxInt16();
//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: virtual short minInt16() const = 0;
  public abstract short minInt16();

//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: virtual int maxInt32() const = 0;
  public abstract int maxInt32();
//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: virtual int minInt32() const = 0;
  public abstract int minInt32();

//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: virtual long maxInt64() const = 0;
  public abstract long maxInt64();
//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: virtual long minInt64() const = 0;
  public abstract long minInt64();

//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: virtual double maxDouble() const = 0;
  public abstract double maxDouble();
//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: virtual double minDouble() const = 0;
  public abstract double minDouble();

//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: virtual float maxFloat() const = 0;
  public abstract float maxFloat();
//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: virtual float minFloat() const = 0;
  public abstract float minFloat();

//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: virtual int toInt(double value) const = 0;
  public abstract int toInt(double value);
//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: virtual int toInt(float value) const = 0;
  public abstract int toInt(float value);

//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: virtual double min(double d1, double d2) const = 0;
  public abstract double min(double d1, double d2);
//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: virtual float min(float f1, float f2) const = 0;
  public abstract float min(float f1, float f2);

//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: int min(int i1, int i2) const
  public int min(int i1, int i2)
  {
	return (i1 < i2) ? i1 : i2;
  }

//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: virtual double max(double d1, double d2) const = 0;
  public abstract double max(double d1, double d2);
//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: virtual float max(float f1, float f2) const = 0;
  public abstract float max(float f1, float f2);

//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: virtual int max(int i1, int i2) const = 0;
  public abstract int max(int i1, int i2);
//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: virtual long max(long l1, long l2) const = 0;
  public abstract long max(long l1, long l2);

//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: virtual double max(double d1, double d2, double d3) const
  public virtual double max(double d1, double d2, double d3)
  {
	return max(max(d1, d2), d3);
  }

//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: virtual float max(float f1, float f2, float f3) const
  public virtual float max(float f1, float f2, float f3)
  {
	return max(max(f1, f2), f3);
  }

//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: virtual float min(float f1, float f2, float f3) const
  public virtual float min(float f1, float f2, float f3)
  {
	return min(min(f1, f2), f3);
  }

//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: virtual double floor(double d) const = 0;
  public abstract double floor(double d);
//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: virtual float floor(float f) const = 0;
  public abstract float floor(float f);

//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: virtual double ceil(double d) const = 0;
  public abstract double ceil(double d);
//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: virtual float ceil(float f) const = 0;
  public abstract float ceil(float f);

//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: virtual double fmod(double d1, double d2) const = 0;
  public abstract double fmod(double d1, double d2);
//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: virtual float fmod(float f1, float f2) const = 0;
  public abstract float fmod(float f1, float f2);

//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: virtual double linearInterpolation(double from, double to, double alpha) const
  public virtual double linearInterpolation(double from, double to, double alpha)
  {
	return from + ((to - from) * alpha);
  }

//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: virtual float linearInterpolation(float from, float to, float alpha) const
  public virtual float linearInterpolation(float from, float to, float alpha)
  {
	return from + ((to - from) * alpha);
  }


//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: virtual double quadraticBezierInterpolation(double from, double middle, double to, double alpha) const
  public virtual double quadraticBezierInterpolation(double from, double middle, double to, double alpha)
  {
	double oneMinusAlpha = 1.0 - alpha;
	return (oneMinusAlpha * oneMinusAlpha * from) + (2.0 * oneMinusAlpha * alpha * middle) + (alpha * alpha * to);
  }

//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: virtual float quadraticBezierInterpolation(float from, float middle, float to, float alpha) const
  public virtual float quadraticBezierInterpolation(float from, float middle, float to, float alpha)
  {
	float oneMinusAlpha = 1.0f - alpha;
	return (oneMinusAlpha * oneMinusAlpha * from) + (2.0f * oneMinusAlpha * alpha * middle) + (alpha * alpha * to);
  }


//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: virtual long doubleToRawLongBits(double value) const = 0;
  public abstract long doubleToRawLongBits(double value);
//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: virtual double rawLongBitsToDouble(long value) const = 0;
  public abstract double rawLongBitsToDouble(long value);

//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: virtual float rawIntBitsToFloat(int value) const = 0;
  public abstract float rawIntBitsToFloat(int value);

//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: virtual double clamp(double value, double min, double max) const
  public virtual double clamp(double value, double min, double max)
  {
	if (value < min)
	{
		return min;
	}
	if (value > max)
	{
		return max;
	}
	return value;
  }

//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: virtual float clamp(float value, float min, float max) const
  public virtual float clamp(float value, float min, float max)
  {
	if (value < min)
	{
		return min;
	}
	if (value > max)
	{
		return max;
	}
	return value;
  }

//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: virtual bool isEquals(double x, double y) const
  public virtual bool isEquals(double x, double y)
  {
	if (x == y)
	{
	  return true;
	}
	const double epsilon = 1e-8;
	return Math.Abs(x - y) <= epsilon * max(Math.Abs(x), Math.Abs(y), 1.0);
  }

//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: virtual bool isEquals(float x, float y) const
  public virtual bool isEquals(float x, float y)
  {
	if (x == y)
	{
	  return true;
	}
	const float epsilon = 1e-8f;
	return Math.Abs(x - y) <= epsilon * max(Math.Abs(x), Math.Abs(y), 1.0f);
  }

//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: virtual bool isBetween(float value, float min, float max) const
  public virtual bool isBetween(float value, float min, float max)
  {
	return (value >= min) && (value <= max);
  }

//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: virtual double pseudoModule(double numerator, double denominator) const
  public virtual double pseudoModule(double numerator, double denominator)
  {

	double result = numerator / denominator;
	long intPart = (long) result; // integer part
	double fracPart = result - intPart; // fractional part

//    if (closeTo(fracPart, 1.0)) {
	if (fracPart == 1.0)
	{
	  return 0;
	}

	return fracPart * denominator;
  }

  /** answer a double value in the range 0.0 (inclusive) and 1.0 (exclusive) */
//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: virtual double nextRandomDouble() const = 0;
  public abstract double nextRandomDouble();

}
