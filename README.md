# Bicycle Mushroom Identification

A small educational Windows Forms application built with C# and .NET 10. The application combines two interactive exercises behind a simple login screen:

1. Calculate a bicycle rental cost from a start and end hour.
2. Identify one of six mushrooms through a sequence of yes/no questions.

This project is intended for learning desktop UI development, event-driven programming, input validation, navigation between forms, and basic decision-tree logic.

## Features

### Login screen

- Accepts a username and password.
- Prevents navigation when either field is empty or contains only whitespace.
- Provides a **Show password** checkbox.
- Displays the currently entered credentials in a status area. The password is masked with asterisks unless **Show password** is selected.
- Pressing Enter activates the **Login** button.

> The login is a local demonstration only. It does not authenticate against a database or remote service, and any non-empty username/password combination is accepted.

### Activity selection

After a successful login, the **Choose** screen provides two activities:

- **Bicycle** — opens the bicycle rental calculator.
- **Mushroom** — opens the mushroom identification questionnaire.

### Bicycle rental calculator

The bicycle activity accepts an inclusive start hour and an exclusive end hour:

- Start hour: `0` through `23`.
- End hour: `1` through `24`.
- Both values must be whole numbers.
- The start hour must be less than the end hour.

The hourly price is calculated as follows:

| Hour range | Rate |
| --- | ---: |
| 00:00–06:59 | 500 RWF/hour |
| 07:00–13:59 | 1,000 RWF/hour |
| 14:00–18:59 | 1,500 RWF/hour |
| 19:00–20:59 | 1,000 RWF/hour |
| 21:00–23:59 | 500 RWF/hour |

The total is calculated one hour at a time, displayed in the read-only total field, and shown in an information message in RWF.

For example, a rental from hour `7` to hour `10` covers hours 7, 8, and 9:

```text
1,000 + 1,000 + 1,000 = 3,000 RWF
```

### Mushroom identification

The mushroom activity uses a five-question decision tree. It identifies one of these six mushrooms:

- Agaric jaunissant
- Amanite tue-mouche
- Cepe de bordeaux
- Coprin chevelu
- Girolle
- Pied bleu

The questions are:

1. Does the mushroom have a ring?
2. If yes, does it grow in a forest?
3. If it does not grow in a forest, does it have a convex cup?
4. If it does not have a ring, does it have gills?
5. If it has gills, does it have a convex cup?

The decision paths are:

| Answers | Result |
| --- | --- |
| Ring: Yes; Forest: Yes | Amanite tue-mouche |
| Ring: Yes; Forest: No; Convex cup: Yes | Agaric jaunissant |
| Ring: Yes; Forest: No; Convex cup: No | Coprin chevelu |
| Ring: No; Gills: No | Cepe de bordeaux |
| Ring: No; Gills: Yes; Convex cup: Yes | Pied bleu |
| Ring: No; Gills: Yes; Convex cup: No | Girolle |

After displaying a result, the questionnaire can be started again without reopening the form.

### Shared window behavior

All application windows inherit common behavior from `BaseForm`:

- Windows open in the center of the screen.
- Windows use a fixed dialog border and cannot be maximized.
- The default font is Segoe UI at 10 points.
- Closing a form exits the application unless the form is being replaced by another form.
- Bicycle and mushroom screens provide **Back** and **Logout** actions.

## Technology stack

- C# 14
- .NET 10 (`net10.0-windows`)
- Windows Forms
- SDK-style .NET project
- Nullable reference types enabled
- Implicit global usings enabled

## Requirements

To build and run the project, use a Windows development environment with:

- Windows 10 or later.
- The .NET 10 SDK with Windows desktop development support.
- Visual Studio 2026 with the .NET desktop development workload, or the .NET CLI.

Because the project targets `net10.0-windows` and uses Windows Forms, it is not intended to run natively on Linux or macOS.

## Getting started

### Clone the repository

```powershell
git clone https://github.com/Fiacre-Cash/lab-two-26854-fiacre-mugisha.git
cd lab-two-26854-fiacre-mugisha
```

### Run with Visual Studio

1. Open `BicycleMushroomIdentification.slnx` in Visual Studio.
2. Confirm that the `BicycleMushroomIdentification` project is selected as the startup project.
3. Build the solution with **Build > Build Solution**.
4. Start the application with **Debug > Start Without Debugging** or press `Ctrl+F5`.
5. Enter any non-empty username and password.
6. Choose either **Bicycle** or **Mushroom**.

### Run with the .NET CLI

From the repository root:

```powershell
dotnet restore
dotnet build
dotnet run --project .\BicycleMushroomIdentification.csproj
```

The application is a graphical Windows application, so it must be run from a Windows desktop session.

## Usage walkthrough

1. Launch the application. The Login window appears.
2. Enter a username and password.
3. Optionally select **Show password** to display the password in the credentials preview.
4. Select **Login**.
5. On the Choose window, select an activity:
   - Select **Bicycle**, enter valid start and end hours, then select **Calculate**.
   - Select **Mushroom**, select **Start**, and answer each question with **Yes** or **No**.
6. Use **Back** to return to activity selection or **Logout** to return to the Login window.

## Project structure

```text
.
├── BicycleMushroomIdentification.slnx  # Visual Studio solution
├── BicycleMushroomIdentification.csproj # .NET 10 Windows Forms project
├── Program.cs                           # Application entry point
├── BaseForm.cs                          # Shared form styling and navigation
├── LoginForm.cs                         # Login and input validation
├── LoginForm.Designer.cs                # Login form resource cleanup code
├── ChooseForm.cs                        # Activity selection screen
├── BicycleForm.cs                       # Bicycle rental price calculator
├── MushroomForm.cs                      # Mushroom identification decision tree
├── .gitignore                            # Ignored IDE and build artifacts
└── README.md                             # Project documentation
```

## Architecture and navigation

`Program.Main` initializes Windows Forms, opens `LoginForm`, and starts the application message loop. Each screen derives from `BaseForm`.

When a user navigates, `BaseForm.GoTo` opens the next form and closes the current one. The `_navigating` flag prevents the application from exiting during that replacement. If a form is closed directly instead of being replaced, `BaseForm.OnFormClosed` exits the application.

The normal navigation flow is:

```text
LoginForm
	└── ChooseForm
		  ├── BicycleForm
		  │     ├── Back → ChooseForm
		  │     └── Logout → LoginForm
		  └── MushroomForm
				├── Back → ChooseForm
				└── Logout → LoginForm
```

## Validation and error handling

The application displays warning message boxes for invalid user input:

- Empty login fields are rejected.
- Bicycle values must be whole numbers.
- The bicycle start hour must be between 0 and 23.
- The bicycle end hour must be between 1 and 24.
- The start hour must be less than the end hour.

The rental total is cleared before each calculation attempt, so invalid input does not leave an old result visible.

## Development notes

- The forms currently create their controls programmatically in their constructors.
- The application does not persist users, rental history, or mushroom results.
- The mushroom names and question paths are defined directly in `MushroomForm.cs`.
- The bicycle rates are defined in the `RateForHour` method in `BicycleForm.cs`.
- Generated build output is placed in `bin/` and `obj/`; these directories are excluded from Git.

## Limitations

This is a compact classroom-style application rather than a production system. In particular:

- Login credentials are not stored or verified.
- Passwords are intentionally exposed when **Show password** is selected and are also shown in the credentials preview.
- No database or external API is used.
- Bicycle rental pricing is based only on whole-hour boundaries.
- Mushroom identification is limited to the six hard-coded options and five questions.
- There are no automated tests or persistent application settings in the current project.

## Building for release

To create a release build with the .NET CLI:

```powershell
dotnet publish .\BicycleMushroomIdentification.csproj -c Release
```

The published output is generated under the project’s `bin\Release\net10.0-windows\` directory. The exact runtime files depend on the .NET SDK and publish options used.

## Repository

GitHub: [Fiacre-Cash/lab-two-26854-fiacre-mugisha](https://github.com/Fiacre-Cash/lab-two-26854-fiacre-mugisha)

## License

No license file is currently included in the repository. Add a license if this project is intended for redistribution or reuse.
