using UnityEngine;
using UnityEngine.UI;
using Klak.TestTools;
using BodyPix;

public sealed class Compositor : MonoBehaviour
{
    [SerializeField] ImageSource _source = null;
    [SerializeField] Shader _shader = null;
    [SerializeField] Texture2D _background = null;
    [SerializeField] RawImage _previewUI = null;
    [SerializeField] ResourceSet _resources = null;

    BodyDetector _detector;
    Material _material;

    void Start()
    {
        _detector = new BodyDetector(_resources, 320, 240);
        _material = new Material(_shader);
        _previewUI.material = _material;
    }

    void OnDestroy()
    {
        _detector?.Dispose();
        _detector = null;

        Destroy(_material);
        _material = null;
    }

    void Update()
    {
        _detector.ProcessImage(_source.Texture);

        _material.SetTexture("_BgTexture", _background);
        _material.SetTexture("_CameraTexture", _source.Texture);
        _material.SetTexture("_MaskTexture", _detector.MaskTexture);
    }
}
