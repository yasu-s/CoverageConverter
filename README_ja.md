Coverage Converter とは？
-------  
MsTestを実行後に出力されるカバレッジファイル(.coverage)を  
XMLファイル形式に変換します。  
JenkinsでカバレッジファイルをEmma形式のレポートファイルに変換する時に  
使用すると便利です。  


動作環境
-------  

* .NET Framework 4.5  
* vstest.console.exe or VisualStudio2012以降 で作成されたカバレッジファイル  
※MSTest.exe or VisualStudio2010以前 で作成されたカバレッジファイルでは動作しません。  

コマンドライン引数
-------
<table>
<tr>
  <th>引数</th>
  <th>説明</th>
</tr>
<tr>
  <td>/in:[ ファイルパス or ファイルパターン ]</td>
  <td>
    入力対象の ファイルパス または ファイルパターン を指定します。<br />
    入力例：/in:data.coverage or /in:TestResult\*.coverage
  </td>
</tr>
<tr>
  <td>/out:[ ファイルパス ]</td>
  <td>
    出力対象のファイルパスを指定します。<br />
    入力例：/out:data.xml
  </td>
</tr>
<tr>
  <td>/symbols:[ ディレクトリ ]</td>
  <td>
    デバッグシンボルが配置されているディレクトリを指定します。<br />
    入力例：/symbols:TestResult\Out
  </td>
</tr>
<tr>
  <td>/exedir:[ ディレクトリ ]</td>
  <td>
    カバレッジ取得対象の実行ファイルが配置されているディレクトリを指定します。<br />
    入力例：/exedir:TestResult\Out
  </td>
</tr>
<tr>
  <td>/xsl:[ ファイルパス ]</td>
  <td>
    XML出力時に変換を行いたい場合、XSL形式のファイルを指定します。<br />
    入力例：/xsl:MSTestCoverageToEmma.xsl
  </td>
</tr>
</table>



実行例
-------
入力ファイル：data.coverage  
出力ファイル：data.xml の場合  

<code>CoverageConverter.exe /in:data.coverage /out:data.xml</code>



Emma形式に変換
------- 
下記からMSTestCoverageToEmma.xslをダウンロードをして、  
実行時に「/xsl:」に指定して下さい。  
https://github.com/jenkinsci/mstest-plugin/blob/master/src/main/resources/hudson/plugins/mstest/MSTestCoverageToEmma.xsl  

<code>CoverageConverter.exe /in:data.coverage /out:data.xml /xsl:MSTestCoverageToEmma.xsl</code>


Visual Studio 2010 または MSTest.exe のカバレッジファイルでの利用  
-------  

Visual Studio 2012
以下のファイルを解凍して利用可能です。
https://github.com/yasu-s/CoverageConverter/releases/download/v1.0/mstest_1.0.zip  