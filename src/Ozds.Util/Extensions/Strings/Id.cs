namespace Ozds.Extensions;

public static partial class Strings
{
  public static string ToStringId(
      this string @this) =>
    @this.RemoveSpecialCharacters();

  public static string CombineIntoStringId(
      params string[] strings) =>
    strings
      .Select(@string => @string.ToStringId())
      // TODO: more succinct?
      .Aggregate("", (current, next) => current + next);

  // NOTE: shamelessly copied from https://stackoverflow.com/a/70555841/4348107
  public static string RemoveSpecialCharacters(
      this string @this) =>
    @this
      .AsSpan()
      .RemoveSpecialCharacters()
      .ToString();

  // NOTE: shamelessly copied from https://stackoverflow.com/a/70555841/4348107
  public static ReadOnlySpan<char> RemoveSpecialCharacters(
      this ReadOnlySpan<char> @this)
  {
    Span<char> buffer = new char[@this.Length];
    int index = 0;

    foreach (char currentChar in @this)
    {
      if (char.IsLetterOrDigit(currentChar))
      {
        buffer[index] = currentChar;
        index++;
      }
    }

    return buffer.Slice(0, index);
  }
}
