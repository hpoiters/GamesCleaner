# GamesCleaner v2.0.0 — Release validation

The final RC4 executable was validated on 2 October 2026 using the same large regression set as the preceding reference run.

## Test set

- 63 selected PGN files
- 38.27 GB selected source data
- Lichess Elite collections 2021–2026
- MegaBase(2K+).pgn
- elite_2400_decisive.pgn

## Settings

- Minimum Elo both players: 2400
- Minimum full moves: 20
- Reject Bullet: yes
- Reject very fast games: yes
- Very-fast base-time threshold: 120 seconds
- Maximum worker threads: 6

## Result

- Status: PASSED / GESLAAGD
- Duration: 00:02:23
- Total processed: 33,825,689
- StrongGames: 19,094,481
- Rejected: 14,731,208
- Malformed/damaged PGN: 0
- Games without Event tag: 0

Control invariant:

`19,094,481 + 14,731,208 = 33,825,689`

The result exactly reproduced the previous large reference run. The original source PGN files were read only and were not modified.

## UI validation

RC4 was checked while processing with the application window at normal, wide and narrow sizes. The progress/status line resizes with the window, remains one line high, and does not overlap adjacent controls.
