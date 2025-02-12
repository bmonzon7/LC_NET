﻿using NUnit.Framework;
using OpenQA.Selenium;

namespace Automation_Framework
{
    [TestFixture("parallel", "chrome")]
  [TestFixture("parallel", "firefox")]
  [TestFixture("parallel", "safari")]
  [TestFixture("parallel", "ie")]
  [Parallelizable(ParallelScope.Fixtures)]
  public class ParallelTest : SingleTest
  {
    public ParallelTest(string profile, string environment) : base(profile, environment) { }
  }
}
