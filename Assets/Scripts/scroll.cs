using UnityEngine;

public class scroll : MonoBehaviour
{
    public float scrollspeed;
    public Material material;
    private Vector2 _scrollingVector;
    private int MainTex = Shader.PropertyToID("_MainTex");

    private void Awake()
    {
        _scrollingVector = new Vector2();
    }
    private void Update()
    {
        var offset = Time.time * scrollspeed;
        _scrollingVector.y = -offset;
        material.SetTextureOffset(MainTex, _scrollingVector);
    }
}
