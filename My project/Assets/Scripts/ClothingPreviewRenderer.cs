using UnityEngine;
using UnityEngine.UI;

public class ClothingPreviewRenderer : MonoBehaviour
{
    [Header("Preview Stage")]
    public Camera previewCamera;
    public Transform previewStage;
    public LayerMask previewLayer;

    [Header("Lighting")]
    public Light previewLight;

    private GameObject previewObject;
    private SkinnedMeshRenderer previewSMR;

    void Awake()
    {
        
        previewObject = new GameObject("PreviewMesh");
        previewObject.layer = LayerMaskToLayer(previewLayer);
        previewObject.transform.SetParent(previewStage);
        previewObject.transform.localPosition = Vector3.zero;

        previewSMR = previewObject.AddComponent<SkinnedMeshRenderer>();
    }

    public void RenderItemToTexture(ClothingItem item, RenderTexture targetRT)
    {
        if (item == null)
        {
            ClearTexture(targetRT);
            return;
        }

   
        previewSMR.sharedMesh = item.mesh;
        previewSMR.sharedMaterials = item.materials;

        FrameItem();

      
        previewCamera.targetTexture = targetRT;
        previewCamera.Render();

      
        previewCamera.targetTexture = null;
    }

    void FrameItem()
    {
        if (previewSMR.sharedMesh == null) return;

      
        Bounds bounds = previewSMR.bounds;
        float objectSize = Mathf.Max(bounds.size.x, bounds.size.y, bounds.size.z);
        float distance = objectSize / (2f * Mathf.Tan(previewCamera.fieldOfView * 0.5f * Mathf.Deg2Rad));

        previewCamera.transform.position = bounds.center - previewCamera.transform.forward * (distance * 1.5f);
    }

    void ClearTexture(RenderTexture rt)
    {
        RenderTexture prev = RenderTexture.active;
        RenderTexture.active = rt;
        GL.Clear(true, true, Color.clear);
        RenderTexture.active = prev;
    }

    int LayerMaskToLayer(LayerMask mask)
    {
        int layerNumber = 0;
        int layer = mask.value;
        while (layer > 1) { layer >>= 1; layerNumber++; }
        return layerNumber;
    }
}
