# Take-Home Assignment: Repository Insights API

You are receiving a working starter codebase for a **Repository Insights API**. The API currently supports registering GitHub repositories to track locally, listing them, and fetching individual entries.

Your task is to **extend** this codebase, not rewrite it.

**Time expectation:** 30 to 60 minutes.

## Your Task

Add the ability to **refresh and persist repository insights** for a tracked repository by fetching data from the GitHub public API.

### Requirements

1. Add a way to fetch and persist repository insights (such as stars, forks, and open issues) from the GitHub public API for a tracked repository.
2. The stored insights should be accessible through the API.
3. Add at least one or two tests covering your changes.

The GitHub public API does not require authentication for reasonable usage. See [GitHub REST API documentation](https://docs.github.com/en/rest/repos/repos#get-a-repository) for reference.

### Guidance

- You do not need to handle GitHub API authentication.
- You do not need to add background jobs or polling. A manual refresh via the endpoint is sufficient.

## Submission

1. **Do not fork this repository.** Forks are publicly visible and other candidates could see your work. Instead, create a new **private** repository under your own GitHub account, copy the contents of this repo into it, and push your changes there.
2. Fill in the [CANDIDATE_NOTES.md](CANDIDATE_NOTES.md) file included in the repository.
3. Add **`lucaazalim-limestone`** as a collaborator on your private repository (Settings > Collaborators).
4. Share your repository URL with the recruitment team.
