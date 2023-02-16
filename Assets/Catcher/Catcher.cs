using UnityEngine;
using UnityEngine.UI;
using Klak.TestTools;
using BodyPix;
using Unity.Mathematics;

public sealed class Catcher : MonoBehaviour
{
    [SerializeField] ImageSource _source = null;
    [SerializeField] ResourceSet _resources = null;
    [SerializeField] Transform _catcher = null;
    [SerializeField] RawImage _previewUI = null;

    BodyDetector _detector;
    OneEuroFilter2 _filter = new OneEuroFilter2();
    float2 _target;

    void Start()
    {
        _detector = new BodyDetector(_resources, 320, 240);

        _filter.Beta = 0.1f;
        _filter.MinCutoff = 0.5f;
    }

    void OnDestroy()
    {
        _detector?.Dispose();
        _detector = null;
    }

    void Update()
    {
        _detector.ProcessImage(_source.Texture);

        var hand = _detector.Keypoints[(int)Body.KeypointID.RightWrist];

        if (hand.Score > 0.8f) _target = hand.Position;

        var filtered = _filter.Step(Time.time, _target);
        filtered = (filtered - 0.5f) * math.float2(-2, 0.8f);
        _catcher.localPosition = math.float3(filtered, 0);

        _previewUI.texture = _source.Texture;
    }
}
