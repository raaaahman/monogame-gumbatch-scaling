/// <summary>
/// Entry point for the Content Builder project, 
/// which when executed will build content according to the "Content Collection Strategy" defined in the Builder class.
/// </summary>
/// <remarks>
/// Make sure to validate the directory paths in the "ContentBuilderParams" for your specific project.
/// For more details regarding the Content Builder, see the MonoGame documentation: <tbc.>
/// </remarks>

using Microsoft.Xna.Framework.Content.Pipeline;
using MonoGame.Framework.Content.Pipeline.Builder;

var contentCollectionArgs = new ContentBuilderParams()
{
    Mode = ContentBuilderMode.Builder,
    WorkingDirectory = $"{AppContext.BaseDirectory}../../../", // path to where your content folder can be located
    SourceDirectory = "Assets", // Not actually needed as this is the default, but added for reference
    Platform = TargetPlatform.DesktopGL
};
var builder = new Builder();

if (args is not null && args.Length > 0)
{
  builder.Run(args);

  ContentBuilderParams parameters = ContentBuilderParams.Parse(args);

  Builder.PostBuild(parameters);
}
else
{
  builder.Run(contentCollectionArgs);

  Builder.PostBuild(contentCollectionArgs);
}

return builder.FailedToBuild > 0 ? -1 : 0;



public class Builder : ContentBuilder
{
  public static void PostBuild(ContentBuilderParams parameters)
  {
    string inputPath = Path.Combine(parameters.WorkingDirectory, "MonoGameContentBuilder", "Assets", "Fonts", "font-16x16.fnt");
    string outputPath = Path.Combine(parameters.RootedOutputDirectory, "Content", "font-16x16.fnt");

    if (File.Exists(outputPath))
    {
      File.Delete(outputPath);
    }
    File.Copy(inputPath, outputPath);
}

  public override IContentCollection GetContentCollection()
  {
      var contentCollection = new ContentCollection();

      contentCollection.Include<WildcardRule>("*");

      contentCollection.IncludeCopy<WildcardRule>("font-*");
      contentCollection.IncludeCopy<WildcardRule>("*.fnt");

      contentCollection.Exclude<WildcardRule>("mockup*");
      contentCollection.Exclude<WildcardRule>("preview*");
      contentCollection.Exclude<WildcardRule>("*.md");
      contentCollection.Exclude<WildcardRule>("*.txt");

      return contentCollection;
  }
}