resource "google_project_service" "artifact-registry" {
  project = var.google-project
  service = "artifactregistry.googleapis.com"
  disable_on_destroy = false
}

resource "google_project_service" "cloud-run" {
  provider                   = google-beta
  project                    = var.google-project
  service                    = "run.googleapis.com"
  disable_on_destroy = false
}

resource "google_project_service" "sql-admin" {
  provider                   = google-beta
  project                    = var.google-project
  service                    = "sqladmin.googleapis.com"
  disable_on_destroy = false
}

resource "google_artifact_registry_repository" "docker-repo" {
  provider = google-beta
  project  = var.google-project
  location = "europe"

  format        = "DOCKER"
  repository_id = "default-docker"
}