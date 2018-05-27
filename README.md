Coverage Converter
-------  

Coverage file(.coverage) that is output after running the MsTest  
I converted to XML file format.   
When you convert to the Emma coverage report file format file in Jenkins  
I find it useful to use.  


Operating environment
-------  

* .NET Framework 4.5  
* Coverage file output by vstest.console.exe or VisualStudio 2012 or later  
※It will not work with coverage files output from MSTest.exe or VisualStudio 2010 or earlier.  

Command line arguments
-------

<table>
<tr>
  <th>argument</th>
  <th>description</th>
</tr>
<tr>
  <td>/in:[ file path or file pattern ]</td>
  <td>
    specify the file path or file pattern to be input. <br />
    example：/in:data.coverage or /in:TestResult\*.coverage  
  </td>
</tr>
<tr>
  <td>/out:[ file path ]</td>
  <td>
    specify the file path of the output target. <br />
    example：/out:data.xml
  </td>
</tr>
<tr>
  <td>/symbols:[ directory ]</td>
  <td>
    specifies the directory where the debug symbols are located. <br />
    example：/symbols:TestResult\Out
  </td>
</tr>
<tr>
  <td>/exedir:[ directory ]</td>
  <td>
    specifies the directory where the executable file to be retrieved coverage is located. <br />
    example：/exedir:TestResult\Out
  </td>
</tr>
<tr>
  <td>/xsl:[ file path ]</td>
  <td>
    If you want to convert the output XML, I want to specify the file format of XSL. <br />
    example：/xsl:MSTestCoverageToEmma.xsl
  </td>
</tr>
</table>



Example
------- 

Input file：data.coverage  
Output file：data.xml   

<code>CoverageConverter.exe /in:data.coverage /out:data.xml</code>



Emma format transform.
------- 

Download from the following MSTestCoverageToEmma.xsl.  
https://github.com/jenkinsci/mstest-plugin/blob/master/src/main/resources/hudson/plugins/mstest/MSTestCoverageToEmma.xsl  

<code>CoverageConverter.exe /in:data.coverage /out:data.xml /xsl:MSTestCoverageToEmma.xsl</code>


Use of coverage file outputted from MSTest.exe or VisualStudio 2010 or earlier  
-------  

Please use the following file.  
https://github.com/yasu-s/CoverageConverter/releases/download/v1.0/mstest_1.0.zip  
