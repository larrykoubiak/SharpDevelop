﻿// <file>
//     <copyright see="prj:///doc/copyright.txt"/>
//     <license see="prj:///doc/license.txt"/>
//     <owner name="Matthew Ward" email="mrward@users.sourceforge.net"/>
//     <version>$Revision$</version>
// </file>

using System;
using System.Diagnostics;
using System.IO;
using ICSharpCode.Core;
using ICSharpCode.SharpDevelop.Project;
using ICSharpCode.UnitTesting;

namespace ICSharpCode.CodeCoverage
{
	public class CodeCoverageTestRunner : TestProcessRunnerBase
	{
		UnitTestingOptions options;
		IFileSystem fileSystem;
		PartCoverApplication partCoverApplication;
		PartCoverSettingsFactory settingsFactory;
		
		public CodeCoverageTestRunner()
			: this(new UnitTestProcessRunner(),
				new TestResultsMonitor(),
				new UnitTestingOptions(),
				new FileSystem())
		{
		}
		
		public CodeCoverageTestRunner(IUnitTestProcessRunner processRunner,
			ITestResultsMonitor testResultsMonitor,
			UnitTestingOptions options,
			IFileSystem fileSystem)
			: base(processRunner, testResultsMonitor)
		{
			this.options = options;
			this.fileSystem = fileSystem;
			settingsFactory = new PartCoverSettingsFactory(fileSystem);
		}
		
		public bool HasCodeCoverageResults()
		{
			return fileSystem.FileExists(CodeCoverageResultsFileName);
		}
		
		public 	CodeCoverageResults ReadCodeCoverageResults()
		{
			TextReader reader = fileSystem.CreateTextReader(CodeCoverageResultsFileName);
			return new CodeCoverageResults(reader);
		}
		
		public string CodeCoverageResultsFileName {
			get { return partCoverApplication.CodeCoverageResultsFileName; }
		}
		
		public override void Start(SelectedTests selectedTests)
		{
			AddProfilerEnvironmentVariableToProcessRunner();
			CreatePartCoverApplication(selectedTests);
			RemoveExistingCodeCoverageResultsFile();
			CreateDirectoryForCodeCoverageResultsFile();
			AppendRunningCodeCoverageMessage();
			
			base.Start(selectedTests);
		}
		
		void AddProfilerEnvironmentVariableToProcessRunner()
		{
			ProcessRunner.EnvironmentVariables.Add("COMPLUS_ProfAPI_ProfilerCompatibilitySetting", "EnableV2Profiler");
		}
		
		void CreatePartCoverApplication(SelectedTests selectedTests)
		{
			NUnitConsoleApplication nunitConsoleApp = new NUnitConsoleApplication(selectedTests, options);
			nunitConsoleApp.Results = base.TestResultsMonitor.FileName;
			
			PartCoverSettings settings = settingsFactory.CreatePartCoverSettings(selectedTests.Project);
			partCoverApplication = new PartCoverApplication(nunitConsoleApp, settings);
		}
		
		void RemoveExistingCodeCoverageResultsFile()
		{
			string fileName = CodeCoverageResultsFileName;
			if (fileSystem.FileExists(fileName)) {
				fileSystem.DeleteFile(fileName);
			}
		}
		
		void CreateDirectoryForCodeCoverageResultsFile()
		{
			string directory = Path.GetDirectoryName(CodeCoverageResultsFileName);
			if (!fileSystem.DirectoryExists(directory)) {
				fileSystem.CreateDirectory(directory);	
			}
		}
		
		void AppendRunningCodeCoverageMessage()
		{
			string message = ParseString("${res:ICSharpCode.CodeCoverage.RunningCodeCoverage}");
			OnMessageReceived(message);
		}
		
		protected virtual string ParseString(string text)
		{
			return StringParser.Parse(text);
		}
		
		protected override ProcessStartInfo GetProcessStartInfo(SelectedTests selectedTests)
		{
			return partCoverApplication.GetProcessStartInfo();
		}
		
		protected override TestResult CreateTestResultForTestFramework(TestResult testResult)
		{
			return new NUnitTestResult(testResult);
		}
	}
}
