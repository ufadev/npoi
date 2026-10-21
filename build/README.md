# ufadev.NPOI

Unofficial fork of [NPOI](https://github.com/nissl-lab/npoi), the .NET port of Apache POI.
Reads and writes `xls`, `xlsx` and `docx`.

- **License:** Apache-2.0 only. No maintenance fee, no EULA, no revenue threshold.
- **Fork point:** upstream commit `aa50262` (2026-03-29), the last revision before upstream added the
  "Open Source Maintenance Fee" EULA to its binary releases.
- **Drop-in:** same assembly names (`NPOI.Core`, `NPOI.OOXML`, `NPOI.OpenXml4Net`, `NPOI.OpenXmlFormats`),
  same namespaces and the same strong-name key as upstream NPOI. Do not reference this package and
  `NPOI` in the same project.
- **Not affiliated** with or endorsed by the NPOI project, Tony Qu or Nissl Lab.

Changes against upstream and the licensing analysis: https://github.com/ufadev/npoi/blob/main/FORK.md

## Install

```
dotnet add package ufadev.NPOI
```

## Quick start

```csharp
using NPOI.XSSF.UserModel;

using var workbook = new XSSFWorkbook();
var sheet = workbook.CreateSheet("Sheet1");
sheet.CreateRow(0).CreateCell(0).SetCellValue("Hello");
using var file = File.Create("hello.xlsx");
workbook.Write(file);
```

Upstream documentation and tutorials apply unchanged: https://github.com/nissl-lab/npoi
