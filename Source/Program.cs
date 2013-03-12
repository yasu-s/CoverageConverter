using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Xml;
using System.Xml.Xsl;
using Microsoft.VisualStudio.CodeCoverage;

namespace Toneri
{
	class Program
	{
		/// <summary>site url</summary>
		private const string SITE_URL = "https://github.com/yasu-s/CoverageConverter";

		/// <summary>argument prefix: Input File Path</summary>
		private const string ARGS_PREFIX_INPUT_PATH = "/in:";

		/// <summary>argument prefix: Output File Path</summary>
		private const string ARGS_PREFIX_OUTPUT_PATH = "/out:";

		/// <summary>argument prefix: Symbols Directory</summary>
		private const string ARGS_PREFIX_SYMBOLS_DIR = "/symbols:";

		/// <summary>argument prefix: Exe Directory</summary>
		private const string ARGS_PREFIX_EXE_DIR = "/exedir:";

		/// <summary>argument prefix: Convert Xsl Path</summary>
		private const string ARGS_PREFIX_XSL_PATH = "/xsl:";

		/// <summary>argument prefix: Help</summary>
		private const string ARGS_HELP = "/?";

		/// <summary>
		/// Main process
		/// </summary>
		/// <param name="args">Command Line Arguments</param>
		static void Main(string[] args)
		{
			bool result = false;

			try
			{
				// cosole write header.
				ConsoleWriteHeader();

				if (IsConsoleWriteHelp(args))
				{
					ConsoleWriteHelp();
					return;
				}

				string inputPath  = ConvertArg(args, ARGS_PREFIX_INPUT_PATH);
				string outputPath = ConvertArg(args, ARGS_PREFIX_OUTPUT_PATH);
				string symbolsDir = ConvertArg(args, ARGS_PREFIX_SYMBOLS_DIR);
				string exeDir     = ConvertArg(args, ARGS_PREFIX_EXE_DIR);
				string xslPath    = ConvertArg(args, ARGS_PREFIX_XSL_PATH);

				if (!File.Exists(inputPath))
				{
					Console.WriteLine("input file not found. ({0})", inputPath);
					return;
				}

				Console.WriteLine("input file: {0}", inputPath);

				string inputDir = Path.GetDirectoryName(inputPath);
				CoverageInfoManager.SymPath = (string.IsNullOrEmpty(symbolsDir)) ? (inputDir) : (symbolsDir);
				CoverageInfoManager.ExePath = (string.IsNullOrEmpty(exeDir)) ? (inputDir) : (exeDir);

				CoverageInfo ci = CoverageInfoManager.CreateInfoFromFile(inputPath);
				CoverageDS data = ci.BuildDataSet(null);

				string outputWk = outputPath;
				if (string.IsNullOrEmpty(outputWk))
					outputWk = Path.ChangeExtension(inputPath, "xml");

				Console.WriteLine("output file: {0}", outputWk);

				if (string.IsNullOrEmpty(xslPath))
					data.WriteXml(outputWk);
				else
					WriteTransformXml(data, outputWk, xslPath);

				result = true;

				Console.WriteLine("convert success.");
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.Message);
			}
			finally
			{
				Environment.Exit((result) ? (0) : (1));
			}
		}

		/// <summary>
		/// Convert Command Line Argument.
		/// </summary>
		/// <param name="args">Command Line Argument</param>
		/// <param name="prefix">target prefix</param>
		/// <returns></returns>
		private static string ConvertArg(string[] args, string prefix)
		{
			if (args != null)
			{
				foreach (string arg in args)
				{
					if ((arg != null) && arg.StartsWith(prefix))
					{
						return arg.Replace(prefix, string.Empty).Replace("\"", string.Empty);
					}
				}
			}
			return string.Empty;
		}

		/// <summary>
		/// Write Transform Xml
		/// </summary>
		/// <param name="data">Coverage DataSet</param>
		/// <param name="outputPath">Output File Path</param>
		/// <param name="xslPath">Xsl File Path</param>
		private static void WriteTransformXml(CoverageDS data, string outputPath, string xslPath)
		{
			Console.WriteLine("xsl file: {0}", xslPath);

			using (XmlReader reader = new XmlTextReader(new StringReader(data.GetXml())))
			{
				using (XmlWriter writer = new XmlTextWriter(outputPath, Encoding.UTF8))
				{
					XslCompiledTransform transform = new XslCompiledTransform();
					transform.Load(xslPath);
					transform.Transform(reader, writer);
				}
			}
		}

		/// <summary>
		/// check write help.
		/// </summary>
		/// <param name="args">Command Line Arguments</param>
		/// <returns>true:write help</returns>
		private static bool IsConsoleWriteHelp(string[] args)
		{
			if ((args == null) || (args.Length == 0))
				return true;

			foreach (string arg in args)
			{
				if (ARGS_HELP.Equals(arg))
					return true;
			}

			return false;
		}

		/// <summary>
		/// Cosole Write Application Header.
		/// </summary>
		private static void ConsoleWriteHeader()
		{
			Assembly asm = Assembly.GetExecutingAssembly();
			AssemblyCopyrightAttribute asmcpy = (AssemblyCopyrightAttribute)Attribute.GetCustomAttribute(asm, typeof(AssemblyCopyrightAttribute));
			Console.WriteLine("{0} [Version {1}]", asm.GetName().Name, asm.GetName().Version);
			Console.WriteLine(asmcpy.Copyright);
			Console.WriteLine("URL: {0}", SITE_URL);
		}

		/// <summary>
		/// Console Write Application Help. 
		/// </summary>
		private static void ConsoleWriteHelp()
		{
			Console.WriteLine();

			if (System.Threading.Thread.CurrentThread.CurrentCulture.Name.IndexOf("ja") >= 0)
			{
				Console.WriteLine("/in:[ ファイルパス ]       入力対象のファイルパスを指定します。");
				Console.WriteLine("/out:[ ファイルパス ]      出力対象のファイルパスを指定します。");
				Console.WriteLine("/symbols:[ ディレクトリ ]  デバッグシンボルが配置されているディレクトリを指定します。");
				Console.WriteLine("/exedir:[ ディレクトリ ]   カバレッジ取得対象の実行ファイルが配置されているディレクトリを指定します。");
				Console.WriteLine("/xsl:[ ファイルパス ]      XML出力時に変換を行いたい場合、XSL形式のファイルを指定します。");
				Console.WriteLine("/?                         ヘルプを表示します。");
			}
			else
			{
				Console.WriteLine("/in:[ file path ]       specify a file path in which you want to enter.");
				Console.WriteLine("/out:[ file path ]      specify the file path of the output target.");
				Console.WriteLine("/symbols:[ directory ]  specifies the directory where the debug symbols are located.");
				Console.WriteLine("/exedir:[ directory ]   specifies the directory where the executable file to be retrieved coverage is located.");
				Console.WriteLine("/xsl:[ file path ]      If you want to convert the output XML, I want to specify the file format of XSL.");
				Console.WriteLine("/?                      Displays the help.");
			}
		}
	}
}
