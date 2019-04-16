using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class AppController : MonoBehaviour
{
    [SerializeField] private ResultsPageController _resultsPage;
    [SerializeField] private SelectAlgorithmPageView _selectAlgorithmPage;
    [SerializeField] private DataPageView _selectDataPage;

    private SettingsData _settings;

    private void Start()
    {
        _settings = new SettingsData
        {
            FreeRectChoiceHeuristic = MaxRectsBinPack.FreeRectChoiceHeuristic.BestShortSideFit,
            Items = new List<Vector2Int>() {
                new Vector2Int(400,400),
                new Vector2Int(800, 600)
            },
            BinSize = new Vector2Int(1000, 1000)
        };

        CalculatePacking();
    }

    public void CalculatePacking()
    {
        var packer = new MaxRectsBinPack(_settings.BinSize.x, _settings.BinSize.y);

        var binsData = new List<BinData>();
        var allRectangles = new List<Rect>(_settings.Items.Where(x => x.x != 0 && x.y != 0).Select(x => new Rect(Vector2.zero, x)));
        var rectanglesLeft = new List<Rect>();
        var oversizedRectangles = new List<Rect>();

        var binSizeRect = new Rect(0, 0, _settings.BinSize.x, _settings.BinSize.y);

        for (int i = allRectangles.Count - 1; i >= 0; i--)
        {
            if (!MaxRectsBinPack.IsContainedIn(allRectangles[i], binSizeRect))
            {
                oversizedRectangles.Add(allRectangles[i]);
                allRectangles.RemoveAt(i);
            }
        }

        for (int i = 0; i < 1000; i++)
        {
            if (allRectangles.Count > 0)
            {
                packer.Init(_settings.BinSize.x, _settings.BinSize.y);
                packer.Insert(allRectangles, rectanglesLeft, _settings.FreeRectChoiceHeuristic);

                var rectsData = new List<RectData>();
                foreach (var r in packer.usedRectangles)
                {
                    rectsData.Add(new RectData() { Position = r.position, Size = r.size });
                }

                var binData = new BinData()
                {
                    Size = new Vector2(_settings.BinSize.x, _settings.BinSize.y),
                    Rects = rectsData.ToArray(),
                    Label = string.Format("Pack {0} : {1}x{2}", i + 1, _settings.BinSize.x, _settings.BinSize.y),
                    Occupancy = packer.Occupancy()
                };

                binsData.Add(binData);
            }
            else
            {
                break;
            }
        }

        _resultsPage.Open(new PackData()
        {
            Bins = binsData.ToArray()
        });
    }

    public void OpenAlgorithmSelectionPage()
    {
        _selectAlgorithmPage.Open(_settings.FreeRectChoiceHeuristic, (algorithm) => _settings.FreeRectChoiceHeuristic = algorithm);
    }

    public void OpenDataPage()
    {
        _selectDataPage.Open(_settings.Items, (items) => _settings.Items = items);
    }
}
