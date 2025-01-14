using System.Collections.Concurrent;
using ObjectDetection;

namespace Kofi.Ogoudi.ObjetDetection;

public class ObjectDetection
{
    private readonly Yolo tinyYolo;

    public ObjectDetection(Yolo tinyYolo)
    {
        tinyYolo = tinyYolo;
    }
    
    public async Task<IList<ObjectDetectionResult>> DetectObjectInScenesAsync(IList<byte[]> imagesSceneData)
    {
        
        var results = new ConcurrentBag<ObjectDetectionResult>();

        
        var tasks = imagesSceneData.Select(async image =>
        {
            // Run detection on each image in a separate Task
            await Task.Run(() =>
            {
                var detectionResult = tinyYolo.Detect(image);
                if (detectionResult != null)
                {
                    results.Add(new ObjectDetectionResult
                    {
                        ImageData = detectionResult.ImageData,
                        Box = detectionResult.Boxes
                    });
                }
            });
        });
        
        await Task.WhenAll(tasks);
        return results.ToList();
    }

}
