using System;

namespace Elasticsearch {
public static class StringExtensions {
  public static string ToStringId(this string str) {
    return str.RemoveSpecialCharacters();
  }

  public static string CombineIntoStringId(params string[] strs) {
    var result = "";
    foreach (var str in strs) {
      result += str.ToStringId();
    }

    return result;
  }

  // TODO: test ids
  // NOTE: shamelessly copied from
  // https://stackoverflow.com/questions/1120198/most-efficient-way-to-remove-special-characters-from-string
  public static string RemoveSpecialCharacters(this string str) {
    return RemoveSpecialCharacters(str.AsSpan()).ToString();
  }

  // NOTE: shamelessly copied from
  // https://stackoverflow.com/questions/1120198/most-efficient-way-to-remove-special-characters-from-string
  public static ReadOnlySpan<char> RemoveSpecialCharacters(
      this ReadOnlySpan<char> str) {
    Span<char> buffer = new char[str.Length];
    int idx = 0;

    foreach (char c in str) {
      if (char.IsLetterOrDigit(c)) {
        buffer[idx] = c;
        idx++;
      }
    }

    return buffer.Slice(0, idx);
  }
}
}
