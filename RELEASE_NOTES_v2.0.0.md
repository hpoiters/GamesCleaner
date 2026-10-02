# GamesCleaner v2.0.0

First public release of GamesCleaner v2 for Windows x64.

## Downloads

- **GamesCleaner_NL.zip** — Dutch interface and Dutch user guide.
- **GamesCleaner_EN.zip** — English interface and English user guide.
- **SHA256SUMS.txt** — checksums for both download packages.

Each language package contains `!GamesCleaner.exe` and its user guide. The executable is self-contained; a separate .NET installation is not required.

## Main features

- Cleans large PGN chess-game collections.
- User-controlled PGN selection; newly discovered PGNs are not automatically preselected.
- Configurable minimum Elo, minimum game length and very-fast time threshold.
- Bullet and recognizable very-fast games can be rejected.
- Multiple independent PGN source files can be processed in parallel.
- Every recognized processed game is written to exactly one of the two PGN outputs.
- Original source PGN files are read only.
- Each completed run gets its own dated result folder and report.
- Window layout remains usable while resizing or maximizing during processing.

## Validation

The final RC4 build was regression-tested on 63 selected PGN files totaling 38.27 GB.

- Total processed: **33,825,689**
- StrongGames: **19,094,481**
- Rejected: **14,731,208**
- Malformed/damaged PGN: **0**
- Duration: **00:02:23**

These counts exactly reproduced the preceding large reference run.
