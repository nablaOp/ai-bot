namespace AiBot.Core.Configuration;

/// <summary>
/// OpenAI API options.
///
/// Extended from <see cref="OpenAI.OpenAiOptions"/>
/// </summary>
public class OpenAiExtendedOptions : OpenAI.OpenAiOptions
{
    public static readonly string SectionName = SettingKey;
    
    public int DefaultMaxTokens { get; set; } = 255;
}