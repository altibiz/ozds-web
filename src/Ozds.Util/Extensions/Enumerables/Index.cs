﻿using System.Runtime.CompilerServices;

namespace Ozds.Util;

public static partial class Enumerables
{
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static IEnumerable<(T, int)> Index<T>(this IEnumerable<T> @this) =>
    @this.Select((item, index) => (item, index));
}
