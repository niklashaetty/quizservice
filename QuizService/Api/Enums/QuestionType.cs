using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Api.Enums;

[JsonConverter(typeof(StringEnumConverter))]
public enum QuestionType
{
    Alternative
}