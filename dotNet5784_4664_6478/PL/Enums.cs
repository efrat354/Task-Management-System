using System.Collections.Generic;
using System;
using System.Collections;

namespace PL;

/// <summary>
/// Represents the Enum of engineer experiences
/// </summary>
internal class Experience : IEnumerable
{
    static readonly IEnumerable<BO.EngineerExperience> s_enums =
(Enum.GetValues(typeof(BO.EngineerExperience)) as IEnumerable<BO.EngineerExperience>)!;
    public IEnumerator GetEnumerator() => s_enums.GetEnumerator();
}
/// <summary>
/// Represents the enumeration of task statuses
/// </summary>
internal class Status : IEnumerable
{
    static readonly IEnumerable<BO.Status> s_enums =
(Enum.GetValues(typeof(BO.Status)) as IEnumerable<BO.Status>)!;
    public IEnumerator GetEnumerator() => s_enums.GetEnumerator();
}
