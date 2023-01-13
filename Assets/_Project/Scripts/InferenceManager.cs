using TMPro;
using UniRx;
using Unity.Barracuda;
using UnityEngine;
using UnityEngine.UI;

namespace _Project
{
    public class InferenceManager : MonoBehaviour
    {
        private const int TexturePixelSize = 28;
        [SerializeField] private NNModel model;
        [SerializeField] private TextMeshProUGUI number;
        [SerializeField] private RawImage image;
        [SerializeField] private Button resetButton;
        [SerializeField] private Paint paint;

        private MnistModel _mnistModel;

        private void Start()
        {
            _mnistModel = new MnistModel(model);
            resetButton.OnClickAsObservable().Subscribe(_ =>
            {
                number.text = string.Empty;
                paint.ResetColors();
            }).AddTo(this);
        }

        private void OnDestroy()
        {
            _mnistModel.Dispose();
        }

        private void Update()
        {
            var colors = (image.texture as Texture2D)?.GetPixels();
            var pixels = new float[TexturePixelSize, TexturePixelSize];

            for (var i = 0; i < TexturePixelSize * TexturePixelSize; i++)
            {
                if (colors != null) pixels[i % TexturePixelSize, i / TexturePixelSize] = colors[i].grayscale;
            }

            var result = _mnistModel.Execute(pixels);
            number.text = result.ToString();
        }
    }
}