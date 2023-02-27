namespace Fibers;

public readonly record struct ProcessData(
    uint Id,
    int Priority,
    int ActiveDuration,
    int TotalDuration
);