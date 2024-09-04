#!/bin/bash

# Default values
settings_file="appsettings.json"
env_file=".env"
environment="Production"

# Parse command line arguments
while [[ "$#" -gt 0 ]]; do
    case $1 in
        -s|--settings) settings_file="$2"; shift ;;
        -e|--env) env_file="$2"; shift ;;
        -env|--environment) environment="$2"; shift ;;
        *) echo "Unknown parameter passed: $1"; exit 1 ;;
    esac
    shift
done

# Determine the correct settings file
if [ "$environment" = "Development" ] || [ "$environment" = "Debug" ]; then
    settings_file="${settings_file%.*}.Development.json"
    environment="Development"
fi

# Check if files exist
if [ ! -f "$settings_file" ]; then
    echo "Settings file not found: $settings_file"
    exit 1
fi

if [ ! -f "$env_file" ]; then
    echo "Env file not found: $env_file"
    exit 1
fi

# Load environment variables
set -a
source "$env_file"
set +a

# Update appsettings.json or appsettings.Development.json using jq
jq --arg host "$DB_HOST" \
   --arg port "$DB_PORT" \
   --arg username "$DB_USERNAME" \
   --arg password "$DB_PASSWORD" \
   --arg database "$DB_NAME" \
   --arg br_username "$BR_USERNAME" \
   --arg br_password "$BR_PASSWORD" \
   --arg br_url "$BR_CHANGES_URL" \
   '.DefaultConnection.Host = $host | 
    .DefaultConnection.Port = $port | 
    .DefaultConnection.Username = $username | 
    .DefaultConnection.Password = $password | 
    .DefaultConnection.Database = $database |
    .BusinessRegisterSettings.Username = $br_username |
    .BusinessRegisterSettings.Password = $br_password |
    .BusinessRegisterSettings.ChangesUrl = $br_url' \
   "$settings_file" > "${settings_file}.tmp" && mv "${settings_file}.tmp" "$settings_file"

jq 'del(.DefaultConnection.Password) | del(.BusinessRegisterSettings.Password)' "$settings_file"
