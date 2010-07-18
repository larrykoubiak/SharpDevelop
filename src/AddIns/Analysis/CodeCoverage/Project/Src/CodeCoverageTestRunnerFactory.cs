﻿// <file>
//     <copyright see="prj:///doc/copyright.txt"/>
//     <license see="prj:///doc/license.txt"/>
//     <owner name="Matthew Ward" email="mrward@users.sourceforge.net"/>
//     <version>$Revision$</version>
// </file>

using System;
using ICSharpCode.UnitTesting;

namespace ICSharpCode.CodeCoverage
{
	public class CodeCoverageTestRunnerFactory : ICodeCoverageTestRunnerFactory
	{	
		public CodeCoverageTestRunner CreateCodeCoverageTestRunner()
		{
			return new CodeCoverageTestRunner();
		}
	}
}
