namespace DjayLanguage.Core.ObjectModels;

/// <summary>
/// Enum with operation error codes.
/// </summary>
public enum ServiceErrorCode
{
    /// <summary>
    /// Success operation.
    /// </summary>
    Ok = 0,

    /// <summary>
    /// Word with the same name already exist.
    /// </summary>
    WordAlreadyExist = 1,

    /// <summary>
    /// Word group with the same name already exist.
    /// </summary>
    WordGroupAlreadyExist = 1,
}

