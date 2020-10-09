using UnityEngine;

public class scroll : MonoBehaviour
{
    public float scrollspeed;
    public Material material;
    private int MainTex = Shader.PropertyToID("_MainTex");

    private void Update()
    {
        var offset = Time.time * scrollspeed;
        material.SetTextureOffset(MainTex, new Vector2(0, -offset));
    }
}
