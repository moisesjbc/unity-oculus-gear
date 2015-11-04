//
//  Angle.cpp
//  G3MiOSSDK
//
//  Created by Diego Gomez Deck on 31/05/12.
//  Copyright (c) 2012 IGO Software SL. All rights reserved.
//

//
//  Angle.hpp
//  G3MiOSSDK
//
//  Created by Diego Gomez Deck on 31/05/12.
//  Copyright (c) 2012 IGO Software SL. All rights reserved.
//



//C++ TO C# CONVERTER NOTE: The following #define macro was replaced in-line:
//ORIGINAL LINE: #define SIN(x) sin(x)
//C++ TO C# CONVERTER NOTE: The following #define macro was replaced in-line:
//ORIGINAL LINE: #define COS(x) cos(x)
//C++ TO C# CONVERTER NOTE: The following #define macro was replaced in-line:
//ORIGINAL LINE: #define TAN(x) tan(x)
//C++ TO C# CONVERTER NOTE: The following #define macro was replaced in-line:
//ORIGINAL LINE: #define NAND NAN
//C++ TO C# CONVERTER NOTE: The following #define macro was replaced in-line:
//ORIGINAL LINE: #define NANF NAN
//C++ TO C# CONVERTER NOTE: The following #define macro was replaced in-line:
//ORIGINAL LINE: #define SIN(x) java.lang.Math.sin(x)
//C++ TO C# CONVERTER NOTE: The following #define macro was replaced in-line:
//ORIGINAL LINE: #define COS(x) java.lang.Math.cos(x)
//C++ TO C# CONVERTER NOTE: The following #define macro was replaced in-line:
//ORIGINAL LINE: #define TAN(x) java.lang.Math.tan(x)
//C++ TO C# CONVERTER NOTE: The following #define macro was replaced in-line:
//ORIGINAL LINE: #define NAND java.lang.Double.NaN
//C++ TO C# CONVERTER NOTE: The following #define macro was replaced in-line:
//ORIGINAL LINE: #define NANF java.lang.Float.NaN
//C++ TO C# CONVERTER NOTE: The following #define macro was replaced in-line:
//ORIGINAL LINE: #define ISNAN(x) (x != x)


//C++ TO C# CONVERTER NOTE: The following #define macro was replaced in-line:
//ORIGINAL LINE: #define TO_RADIANS(degrees) ((degrees) / 180.0 * 3.14159265358979323846264338327950288)
//C++ TO C# CONVERTER NOTE: The following #define macro was replaced in-line:
//ORIGINAL LINE: #define TO_DEGREES(radians) ((radians) * (180.0 / 3.14159265358979323846264338327950288))


public class Angle
{
//  mutable double _sin;
//  mutable double _cos;

  private Angle(double degrees, double radians)
//  _sin(2),
//  _cos(2)
  {
	  this._degrees = degrees;
	  this._radians = radians;
  }


  public readonly double _degrees;
  public readonly double _radians;


  public Angle(Angle angle)
//  _sin(angle._sin),
//  _cos(angle._cos)
  {
	  this._degrees = angle._degrees;
	  this._radians = angle._radians;

  }

  public static Angle fromDegrees(double degrees)
  {
	return new Angle(degrees, ((degrees) / 180.0 * 3.14159265358979323846264338327950288));
  }

  public static Angle fromDegreesMinutes(double degrees, double minutes)
  {
	IMathUtils mu = IMathUtils.instance();
	double sign = (degrees * minutes) < 0 ? - 1.0 : 1.0;
	double d = sign * (mu.abs(degrees) + (mu.abs(minutes) / 60.0));
	return new Angle(d, ((d) / 180.0 * 3.14159265358979323846264338327950288));
  }

  public static Angle fromDegreesMinutesSeconds(double degrees, double minutes, double seconds)
  {
	IMathUtils mu = IMathUtils.instance();
	double sign = (degrees * minutes * seconds) < 0 ? - 1.0 : 1.0;
	double d = sign * (mu.abs(degrees) + (mu.abs(minutes) / 60.0) + (mu.abs(seconds) / 3600.0));
	return new Angle(d, ((d) / 180.0 * 3.14159265358979323846264338327950288));
  }

  public static Angle fromRadians(double radians)
  {
	return new Angle(((radians) * (180.0 / 3.14159265358979323846264338327950288)), radians);
  }

  public static Angle min(Angle a1, Angle a2)
  {
	return (a1._degrees < a2._degrees) ? a1 : a2;
  }

  public static Angle max(Angle a1, Angle a2)
  {
	return (a1._degrees > a2._degrees) ? a1 : a2;
  }

  public static Angle zero()
  {
	return Angle.fromDegrees(0);
  }

  public static Angle pi()
  {
	return Angle.fromDegrees(180);
  }

  public static Angle nan()
  {
		return Angle.fromDegrees(0);
  }

  public static Angle midAngle(Angle angle1, Angle angle2)
  {
	return Angle.fromRadians((angle1._radians + angle2._radians) / 2);
  }

  public static Angle linearInterpolation(Angle from, Angle to, double alpha)
  {
	return Angle.fromRadians((1.0 - alpha) * from._radians + alpha * to._radians);
  }

//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: bool isNan() const
  public bool isNan()
  {
	return (_degrees != _degrees);
  }

//  double sinus() const {
////    if (_sin > 1) {
////      _sin = SIN(_radians);
////    }
////    return _sin;
//    return SIN(_radians);
//  }
//
//  double cosinus() const {
////    if (_cos > 1) {
////      _cos = COS(_radians);
////    }
////    return _cos;
//    return COS(_radians);
//  }

//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: double tangent() const
  public double tangent()
  {
		return 0.0f; //TAN(_radians);
  }

//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: bool closeTo(const Angle& other) const
  public bool closeTo(Angle other)
  {
	return (IMathUtils.instance().abs(_degrees - other._degrees) < DefineConstants.THRESHOLD);
  }

//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: Angle add(const Angle& a) const
  public Angle add(Angle a)
  {
	double r = _radians + a._radians;
	return new Angle(((r) * (180.0 / 3.14159265358979323846264338327950288)), r);
  }

//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: Angle sub(const Angle& a) const
  public Angle sub(Angle a)
  {
	double r = _radians - a._radians;
	return new Angle(((r) * (180.0 / 3.14159265358979323846264338327950288)), r);
  }

//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: Angle times(double k) const
  public Angle times(double k)
  {
	double r = k * _radians;
	return new Angle(((r) * (180.0 / 3.14159265358979323846264338327950288)), r);
  }

//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: Angle div(double k) const
  public Angle div(double k)
  {
	double r = _radians / k;
	return new Angle(((r) * (180.0 / 3.14159265358979323846264338327950288)), r);
  }

//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: double div(const Angle& k) const
  public double div(Angle k)
  {
	return _radians / k._radians;
  }

//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: bool greaterThan(const Angle& a) const
  public bool greaterThan(Angle a)
  {
	return (_radians > a._radians);
  }

//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: bool lowerThan(const Angle& a) const
  public bool lowerThan(Angle a)
  {
	return (_radians < a._radians);
  }

//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: Angle clampedTo(const Angle& min, const Angle& max) const
  public Angle clampedTo(Angle min, Angle max)
  {
	if (_radians < min._radians)
	{
	  return min;
	}

	if (_radians > max._radians)
	{
	  return max;
	}

	return this;
  }

//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: bool isBetween(const Angle& min, const Angle& max) const
  public bool isBetween(Angle min, Angle max)
  {
	return (_radians >= min._radians) && (_radians <= max._radians);
  }

//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: Angle nearestAngleInInterval(const Angle& min, const Angle& max) const
  public Angle nearestAngleInInterval(Angle min, Angle max)
  {
	// it the interval contains the angle, return this value
	if (greaterThan(min) && lowerThan(max))
	{
	  return (this);
	}

	// look for the extreme closest to the angle
	Angle dif0 = distanceTo(min);
	Angle dif1 = distanceTo(max);
	return (dif0.lowerThan(dif1))? min : max;
  }

//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: Angle distanceTo(const Angle& other) const
  public Angle distanceTo(Angle other)
  {
	double dif = IMathUtils.instance().abs(_degrees - other._degrees);
	if (dif > 180)
	{
		dif = 360 - dif;
	}
	return Angle.fromDegrees(dif);
  }

//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: Angle normalized() const
  public Angle normalized()
  {
	double degrees = _degrees;
	while (degrees < 0)
	{
	  degrees += 360;
	}
	while (degrees >= 360)
	{
	  degrees -= 360;
	}
	return new Angle(degrees, ((degrees) / 180.0 * 3.14159265358979323846264338327950288));
  }

//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: bool isZero() const
  public bool isZero()
  {
	return (_degrees == 0);
  }

//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: bool isEquals(const Angle& that) const
  public bool isEquals(Angle that)
  {
	IMathUtils mu = IMathUtils.instance();
	return mu.isEquals(_degrees, that._degrees) || mu.isEquals(_radians, that._radians);
  }

#if JAVA_CODE
  public Override public int hashCode()
  {
	final int prime = 31;
	int result = 1;
	int temp;
	temp = Double.doubleToLongBits(_radians);
	result = (prime * result) + (int)(temp ^ (temp >> > 32));
	return result;
  }


  public Override public boolean equals(final object obj)
  {
	if (this == obj)
	{
	  return true;
	}
	if (obj == @null)
	{
	  return false;
	}
	if (getClass() != obj.getClass())
	{
	  return false;
	}
	final Angle other = (Angle) obj;
	if (Double.doubleToLongBits(_radians) != Double.doubleToLongBits(other._radians))
	{
	  return false;
	}
	return true;
  }
#endif

  public void Dispose()
  {

  }

//C++ TO C# CONVERTER WARNING: 'const' methods are not available in C#:
//ORIGINAL LINE: const string description() const
  public string description()
  {
		/*
	IStringBuilder isb = IStringBuilder.newStringBuilder();
	isb.addDouble(_degrees);
  //  isb->addString("�");
	isb.addString("d");
	string s = isb.getString();
	if (isb != null)
		isb.Dispose();
	return s;
	*/
		return "";
  }

#if JAVA_CODE
  public Override public string toString()
  {
	return description();
  }
#endif

}

internal static partial class DefineConstants
{
	public const double THRESHOLD = 1e-5;
	public const double PI = 3.14159265358979323846264338327950288;
	public const double HALF_PI = 1.57079632679489661923132169163975144;
}