resource "google_project_service" "artifact_registry_api" {
  project = var.google-project
  service = "artifactregistry.googleapis.com"
  disable_on_destroy = false
}

resource "google_artifact_registry_repository" "artifact_registry_docker_repo" {
  provider = google-beta
  project  = var.google-project
  location = "europe"

  format        = "DOCKER"
  repository_id = "default-docker"
}
