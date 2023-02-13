resource "google_cloud_run_service" "backend" {
  name     = "backend"
  location = var.region
  project  = var.google-project

  template {
    spec {
      containers {
        image = format("europe-docker.pkg.dev/%s/%s/backend", var.google-project, google_artifact_registry_repository.docker-repo.name)
        env {
          name  = "INSTANCE_CONNECTION_NAME"
          value = google_sql_database_instance.backend.connection_name
        }
        env {
          name  = "APP"
          value = "WebApi"
        }
        ports {
            container_port = 5000
          }
        }
      service_account_name = google_service_account.cloud-run.email
      }
    }
  metadata {
    annotations = {
      "client.knative.dev/user-image"         = "europe-docker.pkg.dev/indagoodhandsdev/default-docker/backend"
      "run.googleapis.com/cloudsql-instances" = google_sql_database_instance.backend.connection_name
      "run.googleapis.com/ingress"            = "all"
    }
  }
  traffic {
    percent         = 100
    latest_revision = true
  }
  autogenerate_revision_name = true
}

data "google_iam_policy" "backend-noauth" {
  binding {
    role = "roles/run.invoker"
    members = [
      "allUsers",
    ]
  }
}

resource "google_cloud_run_service_iam_policy" "backend-noauth" {
  location    = google_cloud_run_service.backend.location
  project     = google_cloud_run_service.backend.project
  service     = google_cloud_run_service.backend.name

  policy_data = data.google_iam_policy.backend-noauth.policy_data
}
