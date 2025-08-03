#!/bin/bash

# Set the PATH to include .NET
export PATH="$PATH:/home/ubuntu/.dotnet"

# Check if .NET is available
if ! command -v dotnet &> /dev/null; then
    echo "Error: .NET SDK is not installed or not in PATH"
    echo "Please install .NET 8.0 SDK first"
    exit 1
fi

echo "Starting C# Web Application..."
echo "The application will be available at the URL shown below:"
echo ""

# Run the application
dotnet run