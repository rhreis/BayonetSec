# BayonetSec Docker Setup

This directory contains the Docker configuration for running BayonetSec in a containerized environment.

## Services

- **API**: ASP.NET Core Web API built from source
- **PostgreSQL**: Database server
- **Redis**: Cache and session store

## Quick Start

1. Copy the environment file:
   ```bash
   cp Docker/.env.example .env
   ```

2. Edit `.env` with your secure passwords and keys.

3. Start the services:
   ```bash
   docker-compose -f Docker/docker-compose.yml up -d
   ```

4. The API will be available at:
   - HTTP: http://localhost:8080
   - HTTPS: https://localhost:8081

## Environment Variables

See `.env.example` for all required environment variables. Make sure to use strong passwords in production.

## Database Migrations

After starting the containers, run migrations if needed:

```bash
docker-compose -f Docker/docker-compose.yml exec api dotnet ef database update
```

## Development

For development with hot reload:

```bash
docker-compose -f Docker/docker-compose.yml up --build
```

## Security Notes

- Change all default passwords in `.env`
- Use HTTPS in production
- Consider using secrets management for sensitive data
- The Redis instance has password protection enabled

## Ports

- API: 8080 (HTTP), 8081 (HTTPS)
- PostgreSQL: 5432
- Redis: 6379

## Volumes

- `postgres_data`: Persistent PostgreSQL data
- `redis_data`: Persistent Redis data