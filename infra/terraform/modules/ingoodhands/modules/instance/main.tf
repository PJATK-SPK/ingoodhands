resource "google_project_service" "project" {
  project = var.google-project
  service = "compute.googleapis.com"
  disable_on_destroy = false
}

resource "google_compute_instance" "main" {
  name                = format("indagoodhands-vm-%s", var.environment)
  machine_type = var.machine-type
  zone         = var.zone

  tags = ["web"]

  boot_disk {
    initialize_params {
      image = "ubuntu-minimal-2004-lts"
    }
  }

  network_interface {
    network = google_compute_network.main.name
    access_config {
      nat_ip = google_compute_address.static.address
    }
  }
  service_account {
    # Google recommends custom service accounts that have cloud-platform scope and permissions granted via IAM Roles.
    email  = google_service_account.service_account.email
    scopes = ["cloud-platform"]
  }
  metadata = {
    ssh-keys = file("${path.module}/keys/marcel.pub")
  }
  #metadata_startup_script = file("../../../../../../docker-compose.cloud.yml")
  //metadata_startup_script = "curl -sSL https://get.docker.com/ | sudo sh | sudo usermod -aG docker indagoodhandsdev"
  }

resource "google_compute_address" "static" {
  name = "ipv4-address"
}
