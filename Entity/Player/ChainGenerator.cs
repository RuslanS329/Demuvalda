using Godot;
using System;

public partial class ChainGenerator : MeshInstance3D
{
    [Export] public Node3D ChainStart;
    [Export] public Node3D ChainEnd;
    [Export] StandardMaterial3D Material;

    public override void _Ready()
    {


    }
    public override void _PhysicsProcess(double delta)
    {

        if (ChainEnd != null && ChainStart != null)

        {
            SurfaceTool st = new();
            st.Begin(Mesh.PrimitiveType.Triangles);

            Vector3 point1 = ChainEnd.GlobalPosition;
            Vector3 point2 = ChainStart.GlobalPosition;

            Vector3 distance = ChainStart.GlobalPosition - ChainEnd.GlobalPosition;
            Vector3 direction = (distance).Normalized();
            Vector3 axis = direction.Cross(Vector3.Right).Normalized();

            Vector3 offset = direction.Rotated(axis, float.Pi * 0.5f);
            Vector3 point3 = ChainStart.GlobalPosition + offset * 0.2f;
            Vector3 point4 = ChainStart.GlobalPosition + offset * 0.2f;
            float dist = distance.Length();
            Vector2[] UVS =
            {
                new Vector2(0f,0f),
                new Vector2(1f,0f),
                new Vector2(0f,1f + dist),
                new Vector2(1f,1f + dist),
            };
            st.SetMaterial(Material);
            st.SetUV(UVS[0]);
            st.AddVertex(point1);
            st.SetUV(UVS[3]);
            st.AddVertex(point2);
            st.SetUV(UVS[2]);
            st.AddVertex(point3);

            st.SetUV(UVS[0]);
            st.AddVertex(point1);
            st.SetUV(UVS[1]);
            st.AddVertex(point4);
            st.SetUV(UVS[2]);
            st.AddVertex(point3);



            st.GenerateNormals();

            Mesh = st.Commit();


        }
    }
}
