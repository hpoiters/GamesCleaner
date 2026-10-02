# GamesCleaner

GamesCleaner v2 is a Windows application for cleaning large collections of chess games in PGN format.

## Release

**GamesCleaner v2.0.0** is the validated first public release.

Separate Dutch and English packages are provided. Each package contains `!GamesCleaner.exe` and the corresponding user guide.

The validated release candidate was tested on 63 selected PGN files (38.27 GB):

- 33,825,689 games processed
- 19,094,481 written to StrongGames
- 14,731,208 rejected
- 0 malformed/damaged PGN records reported
- every recognized input game written to exactly one output file
- original source PGN files read only

See [VALIDATION.md](VALIDATION.md) for the release validation record.

## Performance and workflow

GamesCleaner was designed to replace a sequence of manual filtering operations with one automated procedure.
In the v2.0.0 validation run, 33,825,689 games in 63 PGN files (38.27 GB) were processed in 2 minutes 23 seconds, using 6 worker threads.
In practical use, this automated workflow can be around an order of magnitude faster (approximately 10×) than carrying out comparable filtering and separation manually in chess database programs such as ChessBase or Scid. The exact difference depends on the database, hardware, filters and workflow; the 10× figure is therefore a practical estimate rather than a controlled benchmark.
The speed advantage is only part of the benefit. After the initial settings have been chosen, GamesCleaner performs the complete procedure automatically: it applies the filters, validates results, separates accepted and rejected games, and produces a processing report. This also avoids repeated manual filtering steps in which selection mistakes can occur.
The original PGN files remain read-only throughout processing.

## What GamesCleaner does

GamesCleaner searches selected folders for PGN files and lets the user choose which files to process. New files are never preselected automatically.

A completed run produces two PGN output files plus a report. Together the two PGN outputs contain the complete set of recognized processed games.

Default filtering includes a minimum Elo for both players, a minimum game length, rejection of Bullet games, rejection of recognizable very-fast games, and validation of completed results.

## Documentation

- [Nederlandse handleiding](docs/HANDLEIDING_NL.txt)
- [English user guide](docs/USER_GUIDE_EN.txt)

## Platform

Windows x64. The release executable is self-contained; a separate .NET installation is not required.
