Coverage Converter
-------  

Coverage file that is output after running the MsTest  
I converted to XML file format.   
When you convert to the Emma coverage report file format file in Jenkins  
I find it useful to use.  


Command line arguments
-------

<table>
<tr>
  <th>argument</th>
  <th>description</th>
</tr>
<tr>
  <td>/in:[ file path ]</td>
  <td>
    specify a file path in which you want to enter. <br />
    example：/in:data.coverage
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
http://wiki.hudson-ci.org/pages/viewpageattachments.action?pageId=41878013&metadataLink=true

<code>CoverageConverter.exe /in:data.coverage /out:data.xml /xsl:MSTestCoverageToEmma.xsl</code>
