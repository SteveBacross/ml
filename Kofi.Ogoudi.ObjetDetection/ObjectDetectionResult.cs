
using ObjectDetection;

namespace Kofi.Ogoudi.ObjetDetection;

public record ObjectDetectionResult
{
    public byte[] ImageData { get; set; }
    public IList<BoundingBox> Box { get; set; }
} 