﻿<?xml version="1.0"?>

<xsl:stylesheet version="1.0"
xmlns:xsl="http://www.w3.org/1999/XSL/Transform">

  <xsl:template match="/States">
    <html>
      <body>
        <h2>States</h2>
        <table border="1">
          <tr bgcolor="#9acd32">
            <th>State</th>
            <th>Capital</th>
          </tr>
          <xsl:for-each select="State">
            <tr>
              <td>
                <xsl:value-of select="@Name"/>
              </td>
              <td>
                <xsl:value-of select="@Capital"/>
              </td>
            </tr>
          </xsl:for-each>
        </table>
      </body>
    </html>
  </xsl:template>

</xsl:stylesheet>
