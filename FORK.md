# ufadev.NPOI: fork notes

Unofficial fork of [nissl-lab/npoi](https://github.com/nissl-lab/npoi), published as the NuGet package `ufadev.NPOI`.
It is not affiliated with or endorsed by the NPOI project, Tony Qu or Nissl Lab.

This file explains three things: why the fork exists, why it is lawful, and what was changed. Apache-2.0 §4(b)
requires modified files to carry notices of the changes; the change list below is that notice.

> This is an engineering analysis, not legal advice. Have counsel review it before relying on it for a
> high-stakes decision.

## 1. Why the fork exists

On 2026-03-30 upstream added `OSMFEULA.txt` (commit `aabc46d9f0`), an "Open Source Maintenance Fee" agreement.
From NPOI 2.8.0 on, the official NuGet packages declare `<license type="file">OSMFEULA.txt</license>` with
`requireLicenseAcceptance=true`, in place of `Apache-2.0`. The agreement asks users that earn revenue and have
annual gross revenue of at least US$10,000 to pay a monthly fee for the official binaries. The README also
says the fee is "required to be paid by all organizations or users of any library from this project who
generate revenue".

| NuGet `NPOI` version | Published  | Package license                  |
|----------------------|------------|----------------------------------|
| 2.7.5                | 2025-09-30 | `Apache-2.0` (expression)        |
| 2.7.6                | 2026-02-27 | `Apache-2.0` (expression)        |
| 2.8.0                | 2026-04-03 | `OSMFEULA.txt` (file), acceptance required |
| 2.8.1                | 2026-09-22 | `OSMFEULA.txt` (file), acceptance required |

## 2. Fork point

`aa502622730616471d1cf77e23a2333a955cb624` (2026-03-29, "Merge pull request #1764"). It is the direct parent
of the EULA commit, so it is the newest upstream revision without the EULA or a fee notice in the tree. It is
released 2.7.6 plus 33 commits, which include .NET 10 support, the SkiaSharp graphics backend, XLOOKUP fixes
and performance work. The fork is versioned `2.7.7` to place it between upstream 2.7.6 and 2.8.0.

## 3. Legal basis

1. **Upstream source license.** The `LICENSE` file is Apache-2.0 and has not changed since 2014 (commits
   `6c2e030838` and `6a8132b6e2`). Every source file at the fork point carries an Apache-2.0 header, apart from
   the one file covered in section 4.
2. **The grant is irrevocable.** Apache-2.0 §2 and §3 grant a "perpetual, worldwide, non-exclusive,
   no-charge, royalty-free, irrevocable" copyright and patent license. A later change to the licensing of
   new releases cannot take back rights already granted for code published earlier.
3. **The EULA does not reach this fork, even on its own terms.**
   - It covers only the "Binary Release", meaning pre-compiled binaries that the project itself provides.
   - §3: "The Fee is not a license fee. The Software's source code is licensed … under the OSI License".
   - §4: the OSI license governs where the two conflict, and "User may independently compile binaries from
     the Software's source code without this Agreement".
   - This fork never uses upstream binaries. It is built from source that predates the EULA.
4. **Contributions.** Upstream has no CLA. Under Apache-2.0 §5 contributions come in under the same license,
   so third-party contributions at the fork point are Apache-2.0 too.
5. **Commercial use** of the fork, including in proprietary closed-source products, is permitted with no fee,
   provided the conditions in §4 below are met.

### 3.1 Apache-2.0 conditions and how this fork meets them

| Clause | Requirement | How it is met |
|--------|-------------|---------------|
| §4(a) | Give recipients a copy of the license | `LICENSE` is in the repository and at the root of the nupkg |
| §4(b) | Modified files carry prominent change notices | Section 5 of this file, plus SPDX headers on new and rewritten files |
| §4(c) | Keep the copyright, patent, trademark and attribution notices in the source | All upstream headers are kept unchanged |
| §4(d) | Pass on the NOTICE file | Upstream shipped none, but NPOI ports Apache POI, so `NOTICE` reproduces the Apache POI NOTICE, adds NPOI attribution, and ships in the nupkg |
| §6 | No trademark rights | The NPOI logo is removed as the package icon. The package ID and description use "NPOI" only to name the origin and state that the fork is unofficial |

### 3.2 Residual risks

- **Name.** "NPOI" is used only descriptively ("fork of NPOI"). No trademark-registry search was done. If the
  upstream owner objects, rename the package. The code is unaffected.
- **Assembly identity.** The fork keeps upstream's assembly names, namespaces and `npoi.snk` strong-name key,
  so it is a binary drop-in replacement. Never reference `NPOI` and `ufadev.NPOI` in the same application:
  both contain `NPOI.Core.dll` with the same identity.
- **Redistribution notices.** An application that ships the fork must ship `LICENSE` and `NOTICE`. If it
  bundles the SkiaSharp native libraries, it must also ship SkiaSharp's `THIRD-PARTY-NOTICES.txt`.

## 4. Dependency license audit

Each license below was read from the package's nuspec on nuget.org on 2026-10-21. The audit covers the
fork's resolved dependency graph.

### 4.1 Runtime dependencies (in the nupkg dependency groups)

| Package | Version | License | Commercial use |
|---------|---------|---------|----------------|
| BouncyCastle.Cryptography | 2.6.2 | MIT | yes |
| Enums.NET | 5.0.0 | MIT | yes |
| ExtendedNumerics.BigDecimal | 2025.1001.2.129 | MIT | yes |
| MathNet.Numerics.Signed | 5.0.0 | MIT | yes |
| Microsoft.IO.RecyclableMemoryStream | 3.0.1 | MIT | yes |
| SharpZipLib | 1.4.2 | MIT | yes |
| SkiaSharp (+ NativeAssets.Win32/macOS/Linux.NoDependencies) | 3.119.2 | MIT, native third-party notices (see below) | yes |
| System.Security.Cryptography.Xml / .Pkcs, System.Formats.Asn1 | 10.0.13 | MIT | yes |
| System.Text.Encoding.CodePages (net472, netstandard) | 8.0.0 | MIT | yes |
| ZString | 2.6.0 | MIT | yes |
| System.Memory, System.Buffers, System.ValueTuple, … (transitive BCL polyfills) | 4.x | MIT (.NET Foundation) | yes |

**SkiaSharp native binaries** bundle Skia (BSD-3-Clause), FreeType (FTL, chosen over GPLv2 under its dual
license), HarfBuzz ("Old MIT"), libpng, zlib, expat, libjpeg-turbo, libwebp, the Adobe DNG SDK (royalty-free,
attribution required) and the Mozilla GIF decoder (MPL-1.1/GPL/LGPL tri-license, used unmodified, so MPL-1.1
asks only for notice). All of these allow commercial use. Apps that ship the natives must include SkiaSharp's
`THIRD-PARTY-NOTICES.txt`.

Before the fork point, upstream had already moved from `SixLabors.ImageSharp`/`SixLabors.Fonts` to SkiaSharp.
The Six Labors Split License requires a paid license for companies with more than US$1M revenue when used with
newer ImageSharp versions, and this fork does not depend on it.

### 4.2 Build-time and test-only (not shipped)

| Package | License | Status |
|---------|---------|--------|
| **NuGetizer 1.4.7** | **OSMF EULA** (since NuGetizer 1.4.0; 1.3.x and earlier were MIT) | **Removed.** Upstream bumped it from 1.1.0 to 1.4.7 on 2026-03-24, so the fork point already pulled in a fee-bearing build tool. Packing now uses plain `dotnet pack` |
| Nuke.Common 10.0.0 | MIT | kept |
| Microsoft.SourceLink.GitHub | MIT | kept (central version only) |
| NUnit 4.5.1, NUnit3TestAdapter 6.2.0, Microsoft.NET.Test.Sdk, GitHubActionsTestLogger, BenchmarkDotNet | MIT | kept |
| NSubstitute 5.1.0 | BSD-3-Clause | kept |
| ICSharpCode.TextEditor 3.2.1.6466 | LGPL-2.1 | kept. Used only by the developer tool `tools/DocPartViewer`, which is not built into the package. LGPL does not affect the library |
| Microsoft.NETCore.Platforms 1.1.0, NETStandard.Library 2.0.3 | Microsoft .NET Library license / MIT | implicit SDK packages, metadata only, free to redistribute |

### 4.3 Source vendored inside the repository

| File | Original license | Action |
|------|------------------|--------|
| `main/Util/Collections/StringTokenizer.cs` | MPL-1.1 / LGPL (from iText, Bruno Lowagie and Paulo Soares) | **Rewritten** from the public `java.util.StringTokenizer` contract under Apache-2.0. The public API is unchanged and the file was unused inside the library |
| `ooxml/POIFS/Crypt/Dsig/**` | eID Applet (FedICT), relicensed ASLv2 for Apache POI | kept. Attribution is in `NOTICE` |
| Everything else | Apache-2.0 (Apache POI / NPOI headers) | kept |

## 5. Changes against upstream `aa50262` (Apache-2.0 §4(b) notice)

- `Directory.Build.props`: removed the NuGetizer reference and the per-project pack items. Projects are now
  non-packable by default. Package metadata points at this fork, and the upstream logo icon is gone.
  Version is `2.7.7`.
- `Directory.Packages.props`: removed NuGetizer. Raised `System.Security.Cryptography.Xml`,
  `System.Formats.Asn1` and `System.Security.Cryptography.Pkcs` (pinned transitively) from 8.0.x to 10.0.13.
  This fixes the high-severity advisories GHSA-23rf-6693-g89p, GHSA-37gx-xxp4-5rgx, GHSA-846f-f764-76mx and
  others. At the fork point those advisories turned restore into an error because NuGet audit warnings are
  treated as errors.
- `solution/NPOI.Pack.csproj`: the package ID is now `ufadev.NPOI`. The four assemblies are packed with SDK
  targets. Package dependencies are restated explicitly, and `LICENSE`, `NOTICE` and `README.md` are packed.
- `main/Util/Collections/StringTokenizer.cs`: clean Apache-2.0 reimplementation, as described in section 4.3.
- `testcases/main/Util/TestStringTokenizer.cs`: new tests for the rewritten tokenizer.
- `build/Build.cs`: on non-Windows, tests now run on `net10.0`, because the test projects no longer target
  `net8.0`.
- `build/README.md`: replaced with the package README for this fork.
- `.github/workflows/opencode.yml`: removed. It was upstream's AI-agent workflow, which uses upstream secrets
  and an unpinned `@latest` action.
- `NOTICE`, `FORK.md`: added.
- `README.md`: added a fork banner at the top. The upstream text below it is unchanged.

## 6. Build and pack

```sh
dotnet pack solution/NPOI.Pack.csproj -c Release -o publish
# or the Nuke pipeline used by CI
./build.sh Clean Test Pack
```

The output is `publish/ufadev.NPOI.<version>.nupkg` with `lib/{net472,netstandard2.0,netstandard2.1,net8.0,net10.0}`.

## 7. Taking later upstream fixes

Upstream source added after the fork point is still Apache-2.0. The EULA applies only to upstream's own
binaries (EULA §3 and §4), so cherry-picking individual upstream commits is permitted. When you do:

- never bring in `OSMFEULA.txt`, the README fee notice, or `<PackageLicenseFile>OSMFEULA.txt`;
- never re-add NuGetizer at version 1.4.0 or later;
- re-check new dependencies against section 4 before merging.
