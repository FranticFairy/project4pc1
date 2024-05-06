using GXPEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public struct Vec2
{
    public float x;
    public float y;

    public Vec2(float pX = 0, float pY = 0)
    {
        x = pX;
        y = pY;
    }

    public override string ToString()
    {
        return String.Format("({0},{1})", x, y);
    }

    public void SetXY(float pX, float pY)
    {
        x = pX;
        y = pY;
    }

    public float Length()
    {
        return Mathf.Sqrt(x * x + y * y);
    }

    public void Normalize()
    {
        float len = Length();
        if (len > 0)
        {
            x /= len;
            y /= len;
        }
    }

    public Vec2 Normalized()
    {
        Vec2 result = new Vec2(x, y);
        result.Normalize();
        return result;
    }

    public float Dot(Vec2 other)
    {

        return x * other.x + y * other.y;
    }

    public Vec2 Normal()
    {

        return new Vec2(-y, x).Normalized();
    }

    public void Reflect(Vec2 pNormal, float pBounciness = 1)
    {

        this -= (1 + pBounciness) * Dot(pNormal.Normalized()) * pNormal.Normalized();
    }

    public static Vec2 operator +(Vec2 left, Vec2 right)
    {
        return new Vec2(left.x + right.x, left.y + right.y);
    }

    public static Vec2 operator -(Vec2 left, Vec2 right)
    {
        return new Vec2(left.x - right.x, left.y - right.y);
    }

    public static Vec2 operator *(Vec2 v, float scalar)
    {
        return new Vec2(v.x * scalar, v.y * scalar);
    }

    public static Vec2 operator *(float scalar, Vec2 v)
    {
        return new Vec2(v.x * scalar, v.y * scalar);
    }

    public static Vec2 operator /(Vec2 v, float scalar)
    {
        return new Vec2(v.x / scalar, v.y / scalar);
    }

    public void RotateAroundRadians(float rot, Vec2 rotationPoint)
    {
        this -= rotationPoint;
        RotateRadians(rot);
        this += rotationPoint;
    }
    public void RotateAroundRadians(float rot, float xRotationPoint, float yRotationPoint)
    {

        RotateAroundRadians(rot, new Vec2(xRotationPoint, yRotationPoint));
    }

    public void RotateAroundDegrees(float rot, Vec2 rotationPoint)
    {
        this -= rotationPoint;
        RotateDegrees(rot);
        this += rotationPoint;
    }
    public void RotateAroundDegrees(float rot, float xRotationPoint, float yRotationPoint)
    {

        RotateAroundDegrees(rot, new Vec2(xRotationPoint, yRotationPoint));
    }

    public void RotateRadians(float rot)
    {
        Vec2 eee = this;
        float cos = Mathf.Cos(rot);
        float sin = Mathf.Sin(rot);
        x = cos * eee.x - sin * eee.y;
        y = sin * eee.x + cos * eee.y;
    }
    public void RotateDegrees(float rot)
    {
        RotateRadians(Deg2Rad(rot));
    }

    public float GetAngleRadiansAbs()
    {
        Vec2 aaa = Normalized();
        return Mathf.Acos(aaa.x);
    }
    public float GetAngleRadians()
    {
        return Mathf.Atan2(y, x);
    }

    public float GetAngleDegreesAbs()
    {

        return Rad2Deg(GetAngleRadiansAbs());
    }
    public float GetAngleDegrees()
    {

        return Rad2Deg(GetAngleRadians());
    }

    public void SetAngleDegrees(float deg)
    {

        SetAngleRadians(Deg2Rad(deg));
    }

    public void SetAngleRadians(float rad)
    {
        Vec2 yay = GetUnitVectorRad(rad) * Length();
        SetXY(yay.x, yay.y);
    }

    public static Vec2 RandomUnitVector()
    {

        return GetUnitVectorDeg(Utils.Random((float)0, 360));
    }

    public static Vec2 GetUnitVectorDeg(float deg)
    {

        return GetUnitVectorRad(Deg2Rad(deg));
    }

    public static Vec2 GetUnitVectorRad(float rad)
    {

        return new Vec2(Mathf.Cos(rad), Mathf.Sin(rad));
    }

    public static float Deg2Rad(float degrees)
    {

        return degrees * Mathf.PI / 180;
    }

    public static float Rad2Deg(float rad)
    {

        return rad * 180 / Mathf.PI;
    }
}