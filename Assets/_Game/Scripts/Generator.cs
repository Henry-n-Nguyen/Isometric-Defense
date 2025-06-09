using UnityEngine;

public class Generator : MonoBehaviour
{
    // public
    [Header("Example")]
    public GameObject prefab;

    [Header("Generate Single")]
    public int x = 0;
    public int y = 0;
    [Space(0.5f)]
    public bool IsGenerateSingle = false;

    [Header("Generate Multiple")]
    public int xStart = 0;
    public int yStart = 0;
    public int width = 5;
    public int length = 5;
    [Space(0.5f)]
    public bool IsGenerateMultiple = false;

    // private
    private float widthJump = 0.45f;
    private float lengthJump = 0.25f;
    private float depthJump = 0.1f;

    private float xPos = 0;
    private float yPos = 0;
    private float zPos = 0;

    // Update is called once per frame
    void Update()
    {
        if (IsGenerateMultiple)
        {
            Spawn(xStart, yStart, width, length);
            IsGenerateMultiple = false;
        }

        if (IsGenerateSingle)
        {
            Spawn(x, y);
            IsGenerateSingle = false;
        }
    }

    private void Spawn(int x, int y, int width, int length)
    {
        for (int i = 0; i < length; i++)
        {
            for (int j = 0; j < width; j++)
            {
                Spawn(i + x, j + y);
            }
        } 
    }

    private void Spawn(int xIndex, int yIndex)
    {
        xPos = (yIndex + xIndex) * widthJump + widthJump;
        yPos = (xIndex - yIndex) * lengthJump + lengthJump;
        zPos = (xIndex - yIndex) * depthJump + depthJump;

        Instantiate(prefab, new Vector3(xPos, yPos, zPos), Quaternion.identity, gameObject.transform);
    }
}
