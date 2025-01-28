using Godot;
using System;
using System.Text.Json.Serialization;

namespace ArchitectsInVoid.JsonDataConversion;

public class JTransform
{
    [JsonInclude] public JBasis Basis { get; set; }
    [JsonInclude] public JVector3 Origin { get; set; }

    public JTransform()
    {
        Basis = new JBasis();
        Origin = new JVector3();
    }

    public JTransform(JBasis basis, JVector3 origin)
    {
        Basis = basis;
        Origin = origin;
    }

    public static implicit operator JTransform(Transform3D b) => new JTransform(b.Basis, b.Origin);
    public static implicit operator Transform3D(JTransform j) => new Transform3D(j.Basis, j.Origin);
}

public class JVector3
{
    [JsonInclude] public double X { get; set; }
    [JsonInclude] public double Y { get; set; }
    [JsonInclude] public double Z { get; set; }

    public JVector3(double x, double y, double z)
    {
        X = x; Y = y; Z = z;
    }

    public JVector3()
    {
        X = 0; Y = 0; Z = 0;
    }

    public static implicit operator JVector3(Vector3 v) => new JVector3(v.X, v.Y, v.Z);
    public static implicit operator Vector3(JVector3 j) => new Vector3(j.X, j.Y, j.Z);
}

public class JBasis
{
    [JsonInclude] public JVector3 Collumn0 { get; set; }
    [JsonInclude] public JVector3 Collumn1 { get; set; }
    [JsonInclude] public JVector3 Collumn2 { get; set; }

    public JBasis()
    {
        Collumn0 = new JVector3();
        Collumn1 = new JVector3();
        Collumn2 = new JVector3();
    }

    public JBasis(JVector3 C0, JVector3 C1, JVector3 C2)
    {
        Collumn0 = C0;
        Collumn1 = C1;
        Collumn2 = C2;
    }

    public static implicit operator JBasis(Basis b) => new JBasis(b.Column0, b.Column1, b.Column2);
    public static implicit operator Basis(JBasis j) => new Basis(j.Collumn0, j.Collumn1, j.Collumn2);
}

