using UnityEngine;

public class HandleLayerOrder : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    public int sortingOffset = 1000; 
    public float precision = 100f;  

    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void LateUpdate()
    {
        spriteRenderer.sortingOrder = sortingOffset - Mathf.RoundToInt(transform.position.y * precision);
    }
}
