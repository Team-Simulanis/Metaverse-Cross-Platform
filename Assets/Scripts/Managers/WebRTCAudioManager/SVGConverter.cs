using System.IO;
using UnityEngine;
using UnityEngine.Networking;
using Unity.VectorGraphics;

public static class SVGConverter
{
    public static Sprite ConvertSVGToSprite(UnityWebRequest www)
    {
        string bitString = System.Text.Encoding.UTF8.GetString(www.downloadHandler.data);
        var tessOptions = new VectorUtils.TessellationOptions()
        {
            StepDistance = 100.0f,
            MaxCordDeviation = 0.5f,
            MaxTanAngleDeviation = 0.1f,
            SamplingStepSize = 0.01f
        };

        // Dynamically import the SVG data, and tessellate the resulting vector scene.
        var sceneInfo = SVGParser.ImportSVG(new StringReader(bitString));
        var geoms = VectorUtils.TessellateScene(sceneInfo.Scene, tessOptions);

        // Build a sprite with the tessellated geometry
        Sprite sprite =
            VectorUtils.BuildSprite(geoms, 10.0f, VectorUtils.Alignment.Center, Vector2.zero, 128, true);
        return sprite;
    }
}