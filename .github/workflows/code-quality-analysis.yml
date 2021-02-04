name: code-quality-analysis

on:
  push:
    branches: [master]
    paths-ignore:
      - "*.md"
      - ".editorconfig"
      - "docs/**"

  pull_request:
    branches: [master]
    paths-ignore:
      - "*.md"
      - ".editorconfig"
      - "docs/**"

  schedule:
    - cron: "18 6 * * 1" # every Monday 06:18 UTC

  workflow_dispatch:

jobs:
  analyze:
    name: Analyze
    runs-on: ubuntu-latest

    strategy:
      fail-fast: false
      matrix:
        language: ["csharp"]

    steps:
      - name: Checkout Git repository
        uses: actions/checkout@v2

      - name: Initialize CodeQL
        uses: github/codeql-action/init@v1
        with:
          languages: ${{ matrix.language }}

      - name: Autobuild
        uses: github/codeql-action/autobuild@v1

      - name: Perform CodeQL Analysis
        uses: github/codeql-action/analyze@v1
