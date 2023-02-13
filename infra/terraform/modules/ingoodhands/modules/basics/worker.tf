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
        env {
          name  = "APP"
          value = "Worker"
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
      "run.googleapis.com/ingress"            = "all"
    }
  }
  traffic {
    percent         = 100
    latest_revision = true
  }
  autogenerate_revision_name = true
}

resource "google_cloud_scheduler_job" "worker" {
  name             = "worker"
  description      = "job to trigger worker"
  schedule         = "0 */1 * * *"
  time_zone        = "Europe/Warsaw"
  attempt_deadline = "320s"

  http_target {
    http_method = "POST"
    uri         = "https://worker-ka7w7ys4tq-ew.a.run.app/donate-jobs/set-expired-donations"

    oidc_token {
      service_account_email = google_service_account.worker.email
    }
  }
}
