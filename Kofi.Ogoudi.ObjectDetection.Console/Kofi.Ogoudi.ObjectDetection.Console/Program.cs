using System.Text.Json;
using Kofi.Ogoudi.ObjetDetection;

namespace Kofi.Ogoudi.ObjectDetection.Console
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            // Vérification des arguments
            if (args.Length < 1)
            {
                System.Console.WriteLine("Usage: dotnet run \"C:\\Users\\Steeve\\Documents\\MicrosoftTechnologie\\Kofi.Ogoudi.ObjetDetection\\Kofi.Ogoudi.ObjetDetection\"");

                return;
            }

            var scenesDirectory = args[0];

            // Vérifier si le répertoire existe
            if (!Directory.Exists(scenesDirectory))
            {
                System.Console.WriteLine($"Erreur : le répertoire '{scenesDirectory}' n'existe pas.");
                return;
            }

            // Charger les images du répertoire
            var imageScenesData = new List<byte[]>();
            foreach (var imagePath in Directory.EnumerateFiles(scenesDirectory, "*.jpg")) // Exemple : recherche des fichiers JPG
            {
                var imageBytes = await File.ReadAllBytesAsync(imagePath);
                imageScenesData.Add(imageBytes);
            }

            if (imageScenesData.Count == 0)
            {
                System.Console.WriteLine("Aucune image trouvée dans le répertoire.");
                return;
            }

            // Initialiser l'objet ObjectDetection (assurez-vous d'avoir configuré votre bibliothèque correctement)
            var tinyYolo = new Yolo(); // Remplacez par votre implémentation ou constructeur si nécessaire
            var objectDetection = new Kofi.Ogoudi.ObjetDetection.ObjectDetection(tinyYolo);

            // Exécuter la détection d'objets
            var detectObjectInScenesResults = await objectDetection.DetectObjectInScenesAsync(imageScenesData);

            // Afficher les résultats au format JSON
            foreach (var objectDetectionResult in detectObjectInScenesResults)
            {
                System.Console.WriteLine($"Box: {JsonSerializer.Serialize(objectDetectionResult.Boxes)}");
            }
        }
    }
}
