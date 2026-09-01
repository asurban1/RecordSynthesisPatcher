# Record Synthesis Patcher

Record Synthesis Patcher is a configurable conflict-resolution patcher for Skyrim Special Edition and Anniversary Edition. It lets you choose individual record fields to forward or merge, then creates a patch based on your current load order.

The patcher is designed for users who want precise control without maintaining the same conflict resolutions manually in xEdit after every load-order change.

## Minimum requirements

- Windows 10 or Windows 11, 64-bit
- Skyrim Special Edition or Anniversary Edition
- [Synthesis](https://mutagen-modding.github.io/Synthesis/) 0.36.5 or newer
- [.NET 10 SDK](https://dotnet.microsoft.com/download/dotnet/10.0), x64
- An internet connection for the initial Git download and build

Visual Studio is **not** required. Synthesis downloads and compiles the patcher from Git.

## Installation

1. Install and open Synthesis.
2. Select or create a Skyrim Special Edition pipeline.
3. Add a patcher from a Git repository.
4. Enter this repository URL:

   `https://github.com/asurban1/RecordSynthesisPatcher`

5. Select the `main` branch if Synthesis asks for a branch.
6. Open the patcher's settings and enable the fields you want it to process.
7. Run the Synthesis pipeline after your load order is finalized.

No manual download, Visual Studio project setup, or compilation is necessary.

## What it does

- Forwards a selected field only when the winning override carries the
  inherited/original value while a different value still survives on an
  independent conflict branch.
- Merges supported collections such as keywords, leveled entries, linked references, and region data.
- Handles supported record flags bit by bit so additions and removals can both be preserved.
- Treats blank and null values as real values instead of assuming they are defaults.
- Respects intentional changes and removals made by descendant plugins.
- Registers compatible fields across every supported record type that exposes them.

### Forwarding behavior

Forwarding is deliberately narrow: it repairs a field that was reset to its
inherited baseline by the winning record. For example, if one plugin changes a
field from the original value and a later, independent plugin wins the record
while retaining the original value for that field, RSP forwards the surviving
change. If the winning plugin supplies its own changed value, RSP leaves that
winning value in place.

In this description, *default* means the value inherited from the record's
origin or parent branch. It does not mean a C# data-type default. A blank,
`null`, zero, `false`, or other apparently empty value remains meaningful when
a plugin deliberately changed the field to that value.

Fields are organized alphabetically by record type and field name. Merge
options are marked with **— [ MERGE ]** in the settings.

## Configuration

All fields are disabled by default. Enable only the fields you want the patcher to evaluate.

The patcher is deliberately configurable rather than automatic: different load orders can require different conflict-resolution choices. Review the generated patch in xEdit when adding new field groups or making major changes to your load order.

## Output and master splitting

Synthesis determines the output plugin name from the pipeline group containing the patcher.

If the patch would exceed the normal master limit, enable Synthesis's automatic splitting option. Current Synthesis releases will then divide the output into multiple plugins when necessary.
