resource "google_cloud_run_service" "worker" {
  name     = "worker"
  location = var.region
  project  = var.google-project

  template {
    spec {
      containers {
        image = format("europe-docker.pkg.dev/%s/%s/worker", var.google-project, google_artifact_registry_repository.docker-repo.name)
        env {
          name  = "INSTANCE_CONNECTION_NAME"
          value = google_sql_database_instance.backend.connection_name
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
      "client.knative.dev/user-image"         = "europe-docker.pkg.dev/indagoodhandsdev/default-docker/worker"
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


resource "google_cloud_run_service_iam_policy" "worker-noauth" {
  location    = google_cloud_run_service.worker.location
  project     = google_cloud_run_service.worker.project
  service     = google_cloud_run_service.worker.name

  policy_data = data.google_iam_policy.backend-noauth.policy_data
}
